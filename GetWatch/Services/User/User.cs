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
using GetWatch.Interfaces.Cards;
using GetWatch.Services.Cards;

namespace GetWatch.Services.User
{
    public class User : IUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }

        public bool IsAdmin { get; set; } = false;
        public IShoppingCart? Cart { get; set; }

        public List<ISupportTicket> SupportTickets { get; set; }

        public List<ICartItem>? Transactions { get; set; }
        public List<ICard>? Cards { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        private ICartItemMapper cartItemMapper;
        private ISupportTicketMapper supportTicketMapper;
        private PurchaseMapper purchaseMapper;

        private ShoppingCartMapper cartMapper;

        private ICardMapper cardMapper;

        public User(IUnitOfWork unitOfWork, string name, string email, string password, string phone,bool IsAdmin, Guid id)
        {
            _unitOfWork = unitOfWork;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            this.IsAdmin = IsAdmin;
            Id = id;
            cartMapper = new ShoppingCartMapper(_unitOfWork);
            cartItemMapper = new CartItemMapper(_unitOfWork);
            supportTicketMapper = new SupportTicketMapper(_unitOfWork);
            purchaseMapper = new PurchaseMapper(_unitOfWork);
            cardMapper = new CardMapper(_unitOfWork);

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
            Cart?.RemoveItem(item);
        }
        public void Checkout()
        {

        }
        public void AddSupportTicket(SupportTicket ticket)
        {

        }
        public void AddCard(ICard card)
        {
            if (Cards == null)
            {
                
                throw new InvalidOperationException("Cards cannot be null.");

            }
            Cards.Add(card);
            cardMapper.Insert(card, Id);
           

        }


        
        
    }
}