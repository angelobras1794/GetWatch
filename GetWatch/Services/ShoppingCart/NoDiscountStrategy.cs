using GetWatch.Services.ShoppingCart;

public class NoDiscountStrategy : IDiscountStrategy
{
    public double ApplyDiscount(double total) => total;
}