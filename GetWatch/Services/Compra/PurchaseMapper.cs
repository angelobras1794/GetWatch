using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;
using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.Compra;

using GetWatch.Services.Db.Purchases;


namespace GetWatch.Services.Compra
{
    public class PurchaseMapper : ICartItemMapper
    {
        private readonly IUnitOfWork _unitOfWork;
        private ICartItemFactory _cartItemFactory = new CartItemFactory();
        public PurchaseMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ICartItem> GetAll(Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbPurchases>();
            if (repository == null)
            {
                Console.WriteLine("Repository for DbPurchase is null.");
                return new List<ICartItem>();
            }
            var dbPurchases = repository.GetAll()?.Where(x => x.UserId == userId).ToList() ?? new List<DbPurchases>();

             var PurchaseItems = dbPurchases.Select(dbPurchase =>
             {
                switch (dbPurchase){
                    case DbBluRayPurchase bluRayPurchase:
                        return _cartItemFactory.CreateBluRayItem(bluRayPurchase.Amount, bluRayPurchase.MovieId,bluRayPurchase.Quantity ,bluRayPurchase.Id);
                    case DbRentPurchase rentalPurchase:
                        return _cartItemFactory.CreateRentalItem(rentalPurchase.Amount, rentalPurchase.MovieId,rentalPurchase.Quantity ,rentalPurchase.RentalEndDate,rentalPurchase.Id);
                    case DbTicketPurchase movieTicketPurchase:
                        return _cartItemFactory.CreateTicketItem(movieTicketPurchase.Amount, movieTicketPurchase.MovieId,  movieTicketPurchase.PersonAmount, movieTicketPurchase.Seats ?? Array.Empty<string>(),movieTicketPurchase.Quantity,movieTicketPurchase.Id);
                    default:
                        throw new InvalidOperationException($"Unhandled DbCartItem type: {dbPurchase.GetType().Name}");
                }
             }).ToList();
            return PurchaseItems;
        }
        public ICartItem? Get(Guid id)
        {
            var repository = _unitOfWork.GetRepository<DbPurchases>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbPurchase is null.");
            }
            var dbPurchase = repository.Get(id);
            if (dbPurchase == null)
            {
                return null;
            }
            switch (dbPurchase){
                case DbBluRayPurchase bluRayPurchase:
                    return _cartItemFactory.CreateBluRayItem(bluRayPurchase.Amount, bluRayPurchase.MovieId,bluRayPurchase.Quantity ,bluRayPurchase.Id);
                case DbRentPurchase rentalPurchase:
                    return _cartItemFactory.CreateRentalItem(rentalPurchase.Amount, rentalPurchase.MovieId,rentalPurchase.Quantity,rentalPurchase.RentalEndDate,rentalPurchase.Id);
                case DbTicketPurchase movieTicketPurchase:
                    return _cartItemFactory.CreateTicketItem(movieTicketPurchase.Amount, movieTicketPurchase.MovieId,  movieTicketPurchase.PersonAmount, movieTicketPurchase.Seats ?? Array.Empty<string>(),movieTicketPurchase.Quantity,movieTicketPurchase.Id);
                default:
                    throw new InvalidOperationException($"Unhandled DbCartItem type: {dbPurchase.GetType().Name}");
            }
        }
        public void Insert(ICartItem item,Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbPurchases>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbPurchase is null.");
            }
            var userRepository = _unitOfWork.GetRepository<DbUser>();
            var user = userRepository.Get(userId);
            var dbPurchase = item switch
            {
                BluRayProduct bluRayItem => (DbPurchases)new DbBluRayPurchase
                {
                    Amount = bluRayItem.Price,
                    MovieId = bluRayItem.movieId,
                    Id = bluRayItem.Id,
                    UserId = userId,
                    User = user
                },
                RentalProduct rentalItem => (DbPurchases)new DbRentPurchase
                {
                    Amount = rentalItem.Price,
                    MovieId = rentalItem.movieId,
                    Id = rentalItem.Id,
                    UserId = userId,
                    RentalEndDate = rentalItem.RentDate,
                    User = user
                },
                MovieTicketProduct ticketItem => (DbPurchases)new DbTicketPurchase
                {
                    Amount = ticketItem.Price,
                    MovieId = ticketItem.movieId,
                    Id = ticketItem.Id,
                    UserId = userId,
                    PersonAmount = ticketItem.getPersonAmount(),
                    Seats = ticketItem.getSeats(),
                    User = user
                },
                _ => throw new InvalidOperationException($"Unhandled ICartItem type: {item.GetType().Name}")
            };
            _unitOfWork.Begin();
            repository.Insert(dbPurchase);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }
        public void Remove(ICartItem item)
        {
            var repository = _unitOfWork.GetRepository<DbPurchases>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbPurchase is null.");
            }
            var dbPurchase = repository.Get(item.Id);
            if (dbPurchase == null)
            {
                return;
            }
            _unitOfWork.Begin();
            repository.Delete(dbPurchase);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }
        
    }
}