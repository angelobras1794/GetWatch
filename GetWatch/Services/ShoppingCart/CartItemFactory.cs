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
        public ICartItem CreateBluRayItem(double price, int movieId, Guid id)
        {
            return new BluRayProduct(price, movieId, id);
        }

        public ICartItem CreateRentalItem(double price, int movieId, Guid id, DateTime rentalDate)
        {
            return new RentalProduct(price, movieId, id, rentalDate);
        }
        

        public ICartItem CreateTicketItem(double price, int movieId, Guid id,int personAmount,string [] seats)
        {
            return new MovieTicketProduct(price, movieId, id, personAmount, seats);
        }
        
    }
}