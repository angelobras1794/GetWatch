using System;
using System.Collections.Generic;
using GetWatch.Services.Tickets;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.ShoppingCart;



namespace GetWatch.Interfaces.User
{
    public interface IUser
    {
        Guid Id { get; set; }
        string? Name { get; set; }
        string? Email { get; set; }
        string? Password { get; set; }
        string? Phone { get; set; }
        IShoppingCart? Cart { get; set; }
        List<ISupportTicket>? SupportTickets { get; set; }
        List<ICartItem>? Transactions { get; set; }

        void AddtoCart(ICartItem item);
        void RemoveFromCart(ICartItem item);
        void Checkout();
        void AddSupportTicket(SupportTicket ticket);
    }
}