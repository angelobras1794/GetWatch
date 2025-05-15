using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.Db;


namespace GetWatch.Services.ShoppingCart
{
    public class CartItemFactory : ICartItemFactory
    {
        public ICartItem CreateBluRayItem(double price, int movieId,int Quantity ,Guid id)
        {
            return new BluRayProduct(price, movieId,Quantity ,id);
        }

        public ICartItem CreateRentalItem(double price, int movieId,int Quantity,DateTime? rentalDate,Guid id)
        {
            return new RentalProduct(price, movieId, Quantity, id, rentalDate);
        }
        

        public ICartItem CreateTicketItem(double price, int movieId, int personAmount,string [] seats,int Quantity,Guid id)
        {
            return new MovieTicketProduct(price, movieId, Quantity, id, personAmount, seats);
        }
        
    }
}