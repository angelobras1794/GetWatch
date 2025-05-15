using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.Purchases
{
    public class DbPurchases : DbItem
    {
        public Guid UserId { get; set; } // Foreign key to DbUser
        public DbUser? User { get; set; } // Navigation property
        public double Amount { get; set; }

        public int MovieId { get; set; }

        public int Quantity { get; set; }
        
        
        
        
    }
}