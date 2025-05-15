using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.ShoppingCart;
using GetWatch.Services.Tickets;
using GetWatch.Interfaces.Db;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.Db;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.Compra;
using GetWatch.Interfaces.User;

namespace GetWatch.Services.User
{
    public class User: IUser
    {
        public Guid Id { get; set; }
        public string ?Name { get; set; }
        public string ?Email { get; set; }
        public string ?Password { get; set; }
        public string ?Phone { get; set; }
        public IShoppingCart ?Cart { get; set; }

        public List<ISupportTicket> SupportTickets { get; set; }

        public List<ICartItem> ?Transactions { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        private ICartItemMapper cartItemMapper;
        private ISupportTicketMapper supportTicketMapper;
        private PurchaseMapper purchaseMapper;
        
        private ShoppingCartMapper cartMapper;

        public User(IUnitOfWork unitOfWork, string name, string email, string password, string phone, Guid id)
        {
            _unitOfWork = unitOfWork;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Id = id;
            cartMapper = new ShoppingCartMapper(_unitOfWork);
            cartItemMapper = new CartItemMapper(_unitOfWork);
            supportTicketMapper = new SupportTicketMapper(_unitOfWork);
            purchaseMapper = new PurchaseMapper(_unitOfWork);
            Cart = cartMapper.Get(Id);
            SupportTickets = supportTicketMapper.GetAll(Id);
            Transactions = purchaseMapper.GetAll(Id);

        }


        public void AddtoCart(ICartItem item)
        {
            if (Cart == null)
            {
                throw new InvalidOperationException("Cart cannot be null.");
            }
            cartItemMapper.Insert(item, Cart.Id);

        }

        public void RemoveFromCart(ICartItem item)
        {
            cartItemMapper.Remove(item);
        }
        public void Checkout()
        {
            // Logic to checkout
        }
        public void AddSupportTicket(SupportTicket ticket)
        {
           //
        }


        
        
    }
}