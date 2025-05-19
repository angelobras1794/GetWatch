using System;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.Proxy
{
    public interface ICartItemList
    {

        void AddItem(ICartItem item);
        void RemoveItem(ICartItem item); 
        
        
       
    }
}