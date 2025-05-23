using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.Db;
using GetWatch.Services.Db;

namespace GetWatch.Services.ShoppingCart
{
    public class ShoppingCartMapper : IShoppingCartMapper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartItemMapper _cartItemMapper;

        public ShoppingCartMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _cartItemMapper = new CartItemMapper(unitOfWork);
        }
        public List<IShoppingCart> GetAll()
        {
            var repository = _unitOfWork.GetRepository<DbCart>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCart is null.");
            }
            
            var dbCarts = repository.GetAll()?.ToList() ?? new List<DbCart>();

            var carts = dbCarts.Select(dbCarts =>
                (IShoppingCart)new ShoppingCart
                {
                    Id = dbCarts.Id,
                    Price = dbCarts.TotalPrice,
                    _items = _cartItemMapper.GetAll(dbCarts.Id)

                }
                ).ToList();

            return carts;
        }

        public IShoppingCart? Get(Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbCart>();
            if (repository == null)
            {
                return new ShoppingCart();
            }
            DbCart dbCart = repository.GetAll()?.FirstOrDefault(x => x.UserId == userId);
            if (dbCart == null)
            {
                return null;
            }
            Console.WriteLine($"Found cart with ID: {dbCart.Id}");

            var cart = new ShoppingCart
            {
                Id = dbCart.Id,
                Price = dbCart.TotalPrice,
                _items = _cartItemMapper.GetAll(dbCart.Id)

            };
            return cart;
        }

        public void Insert(IShoppingCart shoppingCart, Guid userId)
        {
            var repository = _unitOfWork.GetRepository<DbCart>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCart is null.");
            }
            var dbCart = new DbCart
            {
                Id = shoppingCart.Id,
                TotalPrice = shoppingCart.Price,
                UserId = userId
            };
            _unitOfWork.Begin();
            repository.Insert(dbCart);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

        }

        public void Remove(IShoppingCart shoppingCart)
        {
            var repository = _unitOfWork.GetRepository<DbCart>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCart is null.");
            }
            var dbCart = repository.Get(shoppingCart.Id);
            if (dbCart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {shoppingCart.Id} not found.");
            }

            _unitOfWork.Begin();
            repository.Delete(dbCart);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
        public void Update(IShoppingCart shoppingCart)
        {
            var repository = _unitOfWork.GetRepository<DbCart>();
            if (repository == null)
            {
                throw new InvalidOperationException("Repository for DbCart is null.");
            }
            var dbCart = repository.Get(shoppingCart.Id);
            if (dbCart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {shoppingCart.Id} not found.");
            }

            dbCart.TotalPrice = shoppingCart.Price;
            _unitOfWork.Begin();
            repository.Update(dbCart);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
        }
    }
        
}