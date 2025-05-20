using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetWatch.Services.Db
{
    public class DbCard : DbItem
    {
        public long cardNumber { get; set; }
        public string cardOwner { get; set; } = string.Empty;
        public string expiryDate { get; set; } = string.Empty;
        public int cvv { get; set; }
        public Guid UserId { get; set; } 
        public DbUser? User { get; set; } 
    }
}