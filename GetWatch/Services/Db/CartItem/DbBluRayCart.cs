using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.CartItem
{
    public class DbBluRayCart : DbCartItem
    {
        public PurchaseType PurchaseType { get; set; } = PurchaseType.BluRay;
        
    }
}