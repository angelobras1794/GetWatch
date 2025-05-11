using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.ShoppingCart
{
    public interface ICartItemFactory
    {
        ICartItem CreateBluRayItem(double price, int movieId, Guid id);
        ICartItem CreateRentalItem(double price, int movieId, Guid id);
        ICartItem CreateTicketItem(double price, int movieId, Guid id,int personAmount,string [] seats);
        
        
    }
}