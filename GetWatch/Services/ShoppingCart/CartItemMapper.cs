using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Interfaces.Db;
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
                throw new InvalidOperationException("Repository for DbCartItem is null.");
            }
            var dbCartItems = repository.GetAll()?.Where(x => x.CartId == cartId).ToList() ?? new List<DbCartItem>();

             var cartItems = dbCartItems.Select(dbCartItem =>
             {
                switch (dbCartItem){
                    case DbBluRayCart bluRayCartItem:
                        return _cartItemFactory.CreateBluRayItem(bluRayCartItem.Price, bluRayCartItem.MovieId, dbCartItem.Id);
                    case DbRentItem rentalCartItem:
                        return _cartItemFactory.CreateRentalItem(rentalCartItem.Price, rentalCartItem.MovieId, dbCartItem.Id);
                    case DbTicketCart movieTicketCartItem:
                        return _cartItemFactory.CreateTicketItem(movieTicketCartItem.Price, movieTicketCartItem.MovieId, dbCartItem.Id, movieTicketCartItem.PersonAmount, movieTicketCartItem.Seats ?? Array.Empty<string>());    
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
                    return _cartItemFactory.CreateBluRayItem(bluRayCartItem.Price, bluRayCartItem.MovieId, dbCartItem.Id);
                case DbRentItem rentalCartItem:
                    return _cartItemFactory.CreateRentalItem(rentalCartItem.Price, rentalCartItem.MovieId, dbCartItem.Id);
                case DbTicketCart movieTicketCartItem:
                    return _cartItemFactory.CreateTicketItem(movieTicketCartItem.Price, movieTicketCartItem.MovieId, dbCartItem.Id, movieTicketCartItem.PersonAmount, movieTicketCartItem.Seats ?? Array.Empty<string>());
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
            var dbCartItem = cartItem switch
            {
                BluRayProduct bluRayCart => (DbCartItem)new DbBluRayCart
                {
                    MovieId = bluRayCart.movieId,
                    Price = bluRayCart.Price,
                    CartId = cartId
                },
                RentalProduct rentalCart => (DbCartItem)new DbRentItem
                {
                    MovieId = rentalCart.movieId,
                    Price = rentalCart.Price,
                    CartId = cartId
                },
                MovieTicketProduct ticketCart => (DbCartItem)new DbTicketCart
                {
                    MovieId = ticketCart.movieId,
                    Price = ticketCart.Price,
                    PersonAmount = ticketCart.getPersonAmount(),
                    Seats = ticketCart.getSeats(),
                    CartId = cartId
                },
                _ => throw new InvalidOperationException($"Unhandled ICartItem type: {cartItem.GetType().Name}")
            };
            
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
        
    }
}