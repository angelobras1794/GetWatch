using GetWatch.Services.ShoppingCart;
public class Product : ICartItem
{
    public int Id{ get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public int Quantity { get; set; } = 1;
    PricingStrategyContext _pricingStrategyContext;

    public Product(int id, string title, double price)
    {
        Id = id;
        Name = title;
        Price = price;
        _pricingStrategyContext = new PricingStrategyContext(new NoDiscountStrategy());
    }
 
    public double GetFinalPrice() => _pricingStrategyContext.CalculatePrice(this) * Quantity;
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy) => _pricingStrategyContext.SetDiscountStrategy(discountStrategy);
}