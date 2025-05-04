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
        public string ?Password { get; set; } 
        public string ? Email { get; set; } 
        public string Phone { get; set; }  = string.Empty;
        public bool IsAdmin { get; set; } = false;

        // One-to-One Relationship: User has one Cart
        public DbCart Cart { get; set; } = new DbCart();

        // One-to-Many Relationship: User has many Support Tickets
        public List<DbSupportTickets> SupportTickets { get; set; } = new List<DbSupportTickets>();

        // One-to-Many Relationship: User has many Transactions
        public List<DbPurchases> Transactions { get; set; } = new List<DbPurchases>();

        
        
       
        
    }
}