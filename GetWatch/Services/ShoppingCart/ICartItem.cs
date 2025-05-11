using GetWatch.Enums;
using GetWatch.Services.ShoppingCart;
namespace GetWatch.Services.ShoppingCart
{
    public interface ICartItem: IDiscountable
    {
        Guid Id { get; set; }
        // string Name { get; set; }

        int Quantity { get; set; }

        int movieId { get; set; }

        
    }
}