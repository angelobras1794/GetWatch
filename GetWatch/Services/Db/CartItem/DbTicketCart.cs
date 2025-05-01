using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.CartItem
{
    public class DbTicketCart : DbCartItem
    {
        public int PersonAmount { get; set; } 
        public string []? Seats { get; set; }
        
        public PurchaseType PurchaseType { get; set; }=PurchaseType.Ticket; 
        
    }
}