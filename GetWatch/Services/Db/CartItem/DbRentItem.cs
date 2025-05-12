using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.CartItem
{
    public class DbRentItem : DbCartItem
    {
        public PurchaseType PurchaseType { get; set; } = PurchaseType.Rental;
        public DateTime RentDate { get; set; }
    }
}