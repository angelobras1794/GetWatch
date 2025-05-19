using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetWatch.Interfaces.Mediator;

using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.Compra;
using GetWatch.Interfaces.ShoppingCart;


namespace GetWatch.Services.Mediator
{
    public class GetWatchMediator : IGetWatchMediator
    {
        private ICommandManager commandManager;
        private ICartItemMapper cartItemMapper;
        private IShoppingCartMapper shoppingCartMapper;

        public GetWatchMediator(ICommandManager commandManager, ICartItemMapper cartItemMapper, IShoppingCartMapper shoppingCartMapper)
        {
            this.commandManager = commandManager;
            this.cartItemMapper = cartItemMapper;
            this.shoppingCartMapper = shoppingCartMapper;

        }

        public void RemoveFromCart(ICartItem cartItem,IShoppingCart shoppingCart)
        {
             RemoveCartItemCommand removeCartItemCommand = new RemoveCartItemCommand(shoppingCart,cartItem, cartItemMapper, shoppingCartMapper);
             commandManager.Execute(removeCartItemCommand);
            
        }
    }
}