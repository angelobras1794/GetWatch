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
        ICartItem CreateBluRayItem(double price, int movieId,int Quantity ,Guid id = new Guid());
        ICartItem CreateRentalItem(double price, int movieId, int Quantity, DateTime? dueDate = null, Guid id = new Guid());
        ICartItem CreateTicketItem(double price, int movieId,int PersonAmount, string[] seats, int Quantity,Guid id = new Guid());
        
        
    }
}