using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.ShoppingCart
{
    public class RentalProduct : Product
    {
        private PurchaseType PurchaseType { get; set; } = PurchaseType.Rental;
        private DateTime RentDate { get; set; }
        public RentalProduct(double price, int movieId, Guid id) : base(price, movieId, id)
        {
            RentDate = DateTime.Now + TimeSpan.FromDays(14);
        }
    }
    
}