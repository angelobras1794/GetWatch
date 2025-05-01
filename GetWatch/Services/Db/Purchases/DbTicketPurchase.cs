using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.Purchases
{
    public class DbTicketPurchase: DbPurchases
    {
        public int PersonAmount { get; set; } 
        public string []? Seats { get; set; }
        
        public PurchaseType PurchaseType { get; set; }=PurchaseType.Ticket; 
    }
}