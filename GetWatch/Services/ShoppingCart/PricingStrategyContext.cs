using GetWatch.Services.ShoppingCart;


public class PricingStrategyContext
{
    private IDiscountStrategy _discountStrategy;

    public PricingStrategyContext(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    public double CalculatePrice(IDiscountable item)
    {
        return _discountStrategy.ApplyDiscount(item.Price);
    }

    public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }
}