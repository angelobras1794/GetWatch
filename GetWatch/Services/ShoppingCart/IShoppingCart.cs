using GetWatch.Services.ShoppingCart;

public interface IShoppingCart
{
    void AddItem(ICartItem item);
    void RemoveItem(ICartItem item);

    void IncreaseQuantity(ICartItem item);
    void DecreaseQuantity(ICartItem item);

    List<ICartItem> GetItems();

    double Price { get; set; }
    Guid Id { get; set; }

}