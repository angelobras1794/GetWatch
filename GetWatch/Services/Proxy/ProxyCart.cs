using System;
using GetWatch.Interfaces.Proxy;
using GetWatch.Services.ShoppingCart;
using GetWatch.Interfaces.ShoppingCart;
namespace GetWatch.Services.Proxy;



public class ProxyCart : ICartItemList
{


    private readonly ICartItemMapper _cartItemMapper;
    private readonly IShoppingCartMapper _shoppingCartMapper;



    private readonly IShoppingCart _shoppingCart;



    public ProxyCart(IShoppingCart _shoppingCart, ICartItemMapper cartItemMapper,IShoppingCartMapper shoppingCartMapper)
    {
        this._shoppingCart = _shoppingCart;
        _shoppingCartMapper = shoppingCartMapper;
        _cartItemMapper = cartItemMapper;

    }

    public void AddItem(ICartItem item)
    {
        _shoppingCart.AddItem(item);
        _cartItemMapper.Insert(item, _shoppingCart.Id);
        _shoppingCartMapper.Update(_shoppingCart);
    }

    public void RemoveItem(ICartItem item)
    {
        _shoppingCart.RemoveItem(item);
        _cartItemMapper.Remove(item);
        _shoppingCartMapper.Update(_shoppingCart);
    }

    public void UpdateItem(ICartItem item)
    {
        var existingItem = _shoppingCart.GetItems().FirstOrDefault(i => i.Id == item.Id);
        if (existingItem == null)
        {

            return;
        }

        if (item.Quantity > existingItem.Quantity)
        {
            _shoppingCart.IncreaseQuantity(item);
        }
        else if (item.Quantity < existingItem.Quantity)
        {
            _shoppingCart.DecreaseQuantity(item);
        }
        _cartItemMapper.Update(item);
        _shoppingCartMapper.Update(_shoppingCart);

    }

    



}