using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Proxy;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Services.Proxy
{
    public class ProxyTransactions : ITransactionsList
    {
        private readonly ICartItemMapper _cartItemMapper;
        private readonly List<ICartItem> _transactions;

        public ProxyTransactions(ICartItemMapper cartItemMapper, List<ICartItem> transactions)
        {
            _cartItemMapper = cartItemMapper;
            _transactions = transactions;
        }

        public void AddItem(ICartItem item,Guid userId)
        {
            _transactions.Add(item);
            _cartItemMapper.Insert(item, userId);
            
        }

        public void RemoveItem(ICartItem item)
        {
            _transactions.Remove(item);
            _cartItemMapper.Remove(item);
        }
        
    }
}