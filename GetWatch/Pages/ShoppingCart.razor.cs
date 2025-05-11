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

        ShoppingCarts.AddItem(new Product(10, 1, new Guid()));
        ShoppingCarts.AddItem(new Product(10, 2, new Guid()));

    }

    public List<ICartItem> cartItems => ShoppingCarts.GetItems();

    public void Increase(ICartItem item) => ShoppingCarts.IncreaseQuantity(item);
    public void Decrease(ICartItem item) => ShoppingCarts.DecreaseQuantity(item);
    public void Remove(ICartItem item) => ShoppingCarts.RemoveItem(item);

    public double Total => ShoppingCarts.Price;
}
