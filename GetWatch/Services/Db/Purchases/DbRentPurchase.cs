using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.Purchases
{
    public class DbRentPurchase : DbPurchases
    {
        PurchaseType PurchaseType { get; set; } = PurchaseType.Rental;

        public DateTime RentalEndDate { get; set; } = DateTime.UtcNow.AddDays(30); // Default to 1 day from now
    }
}