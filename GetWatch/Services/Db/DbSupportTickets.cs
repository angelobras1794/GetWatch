using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Services.Db
{
    public class DbSupportTickets
    {
        public int UserId { get; set; } // Foreign key to DbUser
        public DbUser? User { get; set; } // Navigation property
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public bool IsResolved { get; set; } = false;
    }
}