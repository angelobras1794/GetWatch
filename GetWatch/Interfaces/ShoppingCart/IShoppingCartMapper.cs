using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.SupportTickets;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Interfaces.ShoppingCart
{
    public interface IShoppingCartMapper
    {
        List<IShoppingCart> GetAll();
        IShoppingCart? Get(Guid id);
        void Insert(IShoppingCart shoppingCart,Guid userId);
        void Remove(IShoppingCart shoppingCart);

        
    }
}