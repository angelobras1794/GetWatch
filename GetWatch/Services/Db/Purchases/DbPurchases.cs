using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.Purchases
{
    public class DbPurchases : DbItem
    {
         public int UserId { get; set; } // Foreign key to DbUser
        public DbUser? User { get; set; } // Navigation property
        public decimal Amount { get; set; }

        public int MovieId { get; set; } 
        
        
        
        
    }
}