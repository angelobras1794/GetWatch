using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.Proxy
{
    public interface ITransactionsList
    {
        void AddItem(ICartItem item, Guid userId);
        void RemoveItem(ICartItem item); 

        
    }
}