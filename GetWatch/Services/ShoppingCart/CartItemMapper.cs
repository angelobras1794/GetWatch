using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using GetWatch.Services.Db.CartItem;

namespace GetWatch.Services.ShoppingCart
{
    public class CartItemMapper : ICartItemMapper
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICartItemFactory _cartItemFactory = new CartItemFactory();

        public CartItemMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ICartItem> GetAll(Guid cartId)
        {
            var repository = _unitOfWork.GetRepository<DbCartItem>();
            if (repository == null)
            {
                Console.WriteLine("Repository for DbCartItem is null.");
                return new List<ICartItem>();

            }

            var dbCartItems = repository.GetAll()?.Where(x => x.CartId == cartId).ToList() ?? new List<DbCartItem>();

            var cartItems = dbCartItems.Select(dbCartItem =>
            {
                switch (dbCartItem)
                {
                    case DbBluRayCart bluRayCartItem:
                        return _cartItemFactory.CreateBluRayItem(bluRayCartItem.Price, bluRayCartItem.MovieId, bluRayCartItem.Quantity, dbCartItem.Id);
                    case DbRentItem rentalCartItem:
                        return _cartItemFactory.CreateRentalItem(rentalCartItem.Price, rentalCartItem.MovieId, rentalCartItem.Quantity, rentalCartItem.RentDate, dbCartItem.Id);
                    case DbTicketCart movieTicketCartItem:
                        return _cartItemFactory.CreateTicketItem(movieTicketCartItem.Price, movieTicketCartItem.MovieId, movieTicketCartItem.PersonAmount, movieTicketCartItem.Seats ?? Array.Empty<string>(), movieTicketCartItem.Quantity, dbCartItem.Id);
                    default:
                        throw new InvalidOperationException($"Unhandled DbCartItem type: {dbCartItem.GetType().Name}");
                }
            }).ToList();
            return cartItems;
        }


        public ICartItem? Get(Guid id)
        {
            var repository = _unitOfWork.GetRepository<DbCartItem>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCartItem is null.");
            }
            var dbCartItem = repository.Get(id);
            if (dbCartItem == null)
            {
                return null;
            }

            switch (dbCartItem)
            {
                case DbBluRayCart bluRayCartItem:
                    return _cartItemFactory.CreateBluRayItem(bluRayCartItem.Price, bluRayCartItem.MovieId, bluRayCartItem.Quantity, dbCartItem.Id);
                case DbRentItem rentalCartItem:
                    return _cartItemFactory.CreateRentalItem(rentalCartItem.Price, rentalCartItem.MovieId, rentalCartItem.Quantity, rentalCartItem.RentDate, dbCartItem.Id);
                case DbTicketCart movieTicketCartItem:
                    return _cartItemFactory.CreateTicketItem(movieTicketCartItem.Price, movieTicketCartItem.MovieId, movieTicketCartItem.PersonAmount, movieTicketCartItem.Seats ?? Array.Empty<string>(), movieTicketCartItem.Quantity, dbCartItem.Id);
                default:
                    throw new InvalidOperationException($"Unhandled DbCartItem type: {dbCartItem.GetType().Name}");
            }

        }

        public void Insert(ICartItem cartItem, Guid cartId)
        {
            var repository = _unitOfWork.GetRepository<DbCartItem>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCartItem is null.");
            }
            var repositoryCart = _unitOfWork.GetRepository<DbCart>();
            var Cart = repositoryCart.Get(cartId);

            DbCartItem dbCartItem = null;
            switch (cartItem)
            {
                case BluRayProduct bluRayCartItem:
                    dbCartItem = new DbBluRayCart
                    {
                        Id = bluRayCartItem.Id,
                        CartId = cartId,
                        Price = bluRayCartItem.Price,
                        MovieId = bluRayCartItem.movieId,
                        Quantity = bluRayCartItem.Quantity
                    };
                    break;
                case RentalProduct rentCartItem:
                    dbCartItem = new DbRentItem
                    {
                        Id = rentCartItem.Id,
                        CartId = cartId,
                        Price = rentCartItem.Price,
                        MovieId = rentCartItem.movieId,
                        RentDate = rentCartItem.RentDate,
                        Quantity = rentCartItem.Quantity
                    };
                    break;
                case MovieTicketProduct ticketCartItem:
                    dbCartItem = new DbTicketCart
                    {
                        Id = ticketCartItem.Id,
                        CartId = cartId,
                        Price = ticketCartItem.Price,
                        MovieId = ticketCartItem.movieId,
                        PersonAmount = ticketCartItem.getPersonAmount(),
                        Seats = ticketCartItem.getSeats(),
                        Quantity = ticketCartItem.Quantity
                    };
                    break;
                default:
                    throw new InvalidOperationException($"Unhandled ICartItem type: {cartItem.GetType().Name}");
            }

            _unitOfWork.Begin();
            repository.Insert(dbCartItem);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }

        public void Remove(ICartItem cartItem)
        {
            var repository = _unitOfWork.GetRepository<DbCartItem>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCartItem is null.");
            }
            var dbCartItem = repository.Get(cartItem.Id);
            if (dbCartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with ID {cartItem.Id} not found.");
            }

            _unitOfWork.Begin();
            repository.Delete(dbCartItem);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }
        
        public void Update(ICartItem cartItem)
        {
            var repository = _unitOfWork.GetRepository<DbCartItem>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCartItem is null.");
            }
            var dbCartItem = repository.Get(cartItem.Id);
            if (dbCartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with ID {cartItem.Id} not found.");
            }

            _unitOfWork.Begin();
            dbCartItem.Quantity = cartItem.Quantity;
            repository.Update(dbCartItem);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        
    }
}