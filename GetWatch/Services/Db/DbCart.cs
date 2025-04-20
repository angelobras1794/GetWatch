using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Services.Db
{
    public class DbCart
    {
       
        public Guid UserId { get; set; }
        public DbUser User { get; set; }  = null!;

        public List<DbCartItem> CartItems { get; set; } = new List<DbCartItem>();
        public decimal TotalPrice { get; set; } = 0.0m;
        


        
        
    }
}