using GetWatch.Services.ShoppingCart;

public interface IDiscountStrategy
{
    public double ApplyDiscount(double total);
}