using System;
using GetWatch.Interfaces.Compra;
using GetWatch.Pages;
using GetWatch.Services.ShoppingCart;

public class CommandCarrinho : ICommand
{
   private readonly IShoppingCart _carrinho;
    private readonly ICartItem _item;

    public CommandCarrinho (IShoppingCart carrinho, ICartItem item)
    {
        _carrinho = carrinho;
        _item = item;
    }

    public void Execute()
    {
        _carrinho.RemoveItem(_item); // Remove do carrinho
    }

    public void Undo()
    {
        _carrinho.AddItem(_item); // Recoloca no carrinho
    }

    public void Redo()
    {
        _carrinho.RemoveItem(_item); // Remove novamente
    }
}