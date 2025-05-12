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

namespace GetWatch.Services.User
{
    public class User
    {
        public Guid Id { get; set; }
        public string ?Name { get; set; }
        public string ?Email { get; set; }
        public string ?Password { get; set; }
        public string ?Phone { get; set; }
        public IShoppingCart ?Cart { get; set; }

        public List<SupportTicket> ?SupportTickets { get; set; }

        public List<ICartItem> ?Transactions { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        private ICartItemMapper cartItemMapper;
        private ISupportTicketMapper supportTicketMapper;
        private PurchaseMapper purchaseMapper;

        public User(IUnitOfWork unitOfWork,string name, string email, string password, string phone,Guid id)
        {
            _unitOfWork = unitOfWork;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Id = id;
            cartItemMapper = new CartItemMapper(_unitOfWork);
            supportTicketMapper = new SupportTicketMapper(_unitOfWork);
            purchaseMapper = new PurchaseMapper(_unitOfWork);
        }
        

        public void AddtoCart(ICartItem item){

        }

        public void RemoveFromCart(ICartItem item)
        {
            // Logic to remove item from cart
        }
        public void Checkout()
        {
            // Logic to checkout
        }
        public void AddSupportTicket(SupportTicket ticket)
        {
            // Logic to add support ticket
        }


        
        
    }
}