using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Enums;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;

namespace GetWatch.Services.ShoppingCart
{
    public class BluRayProduct : Product
    {
        private PurchaseType PurchaseType { get; set; } = PurchaseType.BluRay;
        public BluRayProduct(double price,int movieId ,int Quantity, Guid id) : base(price, movieId,Quantity ,id)
        {
            
        }
        
        
    }
}