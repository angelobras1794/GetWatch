using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.Mediator
{
    public interface IGetWatchMediator
    {
        void RemoveFromCart(ICartItem cartItem,IShoppingCart shoppingCart);
        
    }
}