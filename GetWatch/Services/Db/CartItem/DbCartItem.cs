using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;
using GetWatch.Services.Movies;

namespace GetWatch.Services.Db.CartItem
{
    public class DbCartItem : DbItem
    {
        public Guid CartId { get; set; }
        public DbCart? Cart { get; set; }
        
        public int MovieId { get; set; }
        
        public double Price { get; set; } = 0.0;

        public int Quantity { get; set; } = 1;
        
        
        
    }
}