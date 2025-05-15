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
        public DateTime RentDate { get; set; }
        public RentalProduct(double price, int movieId,int Quantity ,Guid id, DateTime? date = null) : base(price, movieId,Quantity ,id)
        {
            RentDate = date ?? DateTime.Now + TimeSpan.FromDays(14);
        }
    }
    
}