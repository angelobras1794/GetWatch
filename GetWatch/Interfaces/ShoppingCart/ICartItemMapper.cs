using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.ShoppingCart
{
    public interface ICartItemMapper
    {
        List<ICartItem> GetAll(Guid cartId);
        ICartItem? Get(Guid id);

        void Insert(ICartItem cartItem, Guid cartId);
        void Remove(ICartItem cartItem);

        void Update(ICartItem cartItem);
        
        
    }
}