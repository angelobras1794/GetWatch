using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.Db;
using GetWatch.Services.Db.Purchases;


namespace GetWatch.Services.Db
{
    public class DbUser : DbItem
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;

        public DbCart Cart { get; set; } = new DbCart();

        public List<DbSupportTickets> SupportTickets { get; set; } = new List<DbSupportTickets>();

        public List<DbPurchases> Transactions { get; set; } = new List<DbPurchases>();
        
        public List<DbCard> Cards { get; set; } = new List<DbCard>();

        
        
       
        
    }
}