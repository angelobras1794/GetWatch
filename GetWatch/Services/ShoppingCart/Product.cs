using GetWatch.Services.ShoppingCart;
using GetWatch.Enums;
public class Product : ICartItem
{
    public Guid Id{ get; set; }
    public double Price { get; set; }

    public int Quantity { get; set; } = 1;

    public int movieId { get; set; }
    
    PricingStrategyContext _pricingStrategyContext;

    

    public Product (double price, int movieId,Guid id = new Guid())
    {
        Id = id;
        this.movieId = movieId;
        Price = price;
       
        _pricingStrategyContext = new PricingStrategyContext(new NoDiscountStrategy());
    }
 
    public double GetFinalPrice() => _pricingStrategyContext.CalculatePrice(this) * Quantity;
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy) => _pricingStrategyContext.SetDiscountStrategy(discountStrategy);
}