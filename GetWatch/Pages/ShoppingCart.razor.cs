using Microsoft.AspNetCore.Components;
using GetWatch.Services.ShoppingCart;
using GetWatch.Services.Db;

public partial class ShoppingCartService : ComponentBase
{
    [Inject]
    private IShoppingCart ShoppingCarts { get; set; }

    protected override void OnInitialized()
    {
        // Ensure cart items are added or fetched

        ShoppingCarts.AddItem(new Product(1, "Product 1", 10.0));
        ShoppingCarts.AddItem(new Product(2, "Product 2", 20.0));

    }

    public List<ICartItem> cartItems => ShoppingCarts.GetItems();

    public void Increase(ICartItem item) => ShoppingCarts.IncreaseQuantity(item);
    public void Decrease(ICartItem item) => ShoppingCarts.DecreaseQuantity(item);
    public void Remove(ICartItem item) => ShoppingCarts.RemoveItem(item);

    public double Total => ShoppingCarts.Price;
}
