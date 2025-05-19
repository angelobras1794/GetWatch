using System;
using GetWatch.Interfaces.Compra;
using GetWatch.Interfaces.ShoppingCart;
using GetWatch.Pages;
using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.Proxy;
using GetWatch.Services.Proxy;

public class RemoveCartItemCommand : ICommand
{
    private readonly ICartItem _item;
    

    public ICartItemList _cartItemList;

    public RemoveCartItemCommand(IShoppingCart carrinho, ICartItem item, ICartItemMapper cartItemMapper, IShoppingCartMapper shoppingCartMapper)
    {
        _item = item;
        _cartItemList = new ProxyCart(carrinho, cartItemMapper, shoppingCartMapper);
        

    }

    public void Execute()
    {
        _cartItemList.RemoveItem(_item); 
    }

    public void Undo()
    {
        _cartItemList.AddItem(_item); 
    }

    public void Redo()
    {

        Execute();
    }
}