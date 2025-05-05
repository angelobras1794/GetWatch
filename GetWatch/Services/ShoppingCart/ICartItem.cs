using GetWatch.Services.ShoppingCart;
namespace GetWatch.Services.ShoppingCart
{
    public interface ICartItem: IDiscountable
    {
        int Id { get; set; }
        string Name { get; set; }

        int Quantity { get; set; }
    }
}