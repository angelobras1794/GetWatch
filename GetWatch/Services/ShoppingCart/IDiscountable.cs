using GetWatch.Services.ShoppingCart;

public interface IDiscountable
{
    double Price { get; set; }
    double GetFinalPrice();
    void SetDiscountStrategy(IDiscountStrategy discountStrategy);
}