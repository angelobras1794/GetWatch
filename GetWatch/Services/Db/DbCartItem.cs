using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db
{
    public class DbCartItem
    {
        public Guid CartId { get; set; }
        public DbCart Cart { get; set; } = null!;
        
        // public Guid MovieId { get; set; }
        // public DbMovie Movie { get; set; } = null!;
        
        public int Quantity { get; set; } = 1;
        
        public decimal Price { get; set; } = 0.0m;
        
        public PurchaseType PurchaseType { get; set; } 
        
    }
}