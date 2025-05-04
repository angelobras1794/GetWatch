using GetWatch.Services.ShoppingCart;

public class PercentageDiscount : IDiscountStrategy
{
    private readonly double _percent;
    public PercentageDiscount(double percent) => _percent = percent;
    public double ApplyDiscount(double total) => total * (1 - _percent);
}