using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;

namespace GetWatch.Services.Db.Purchases
{
    public class DbBluRayPurchase : DbPurchases
    {

        PurchaseType PurchaseType { get; set; } = PurchaseType.BluRay;
        OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }
}