using GetWatch.Services.ShoppingCart;

public class ShoppingCart : IDiscountable
{
    private List<ICartItem> _items;
    private PricingStrategyContext _pricingStrategyContext;
    public double Price {get; set; }

    public ShoppingCart()
    {
        _items = new List<ICartItem>();
        _pricingStrategyContext = new PricingStrategyContext(new NoDiscountStrategy());
    }
    private void UpdatePrice() => Price = _items.Sum(item => item.GetFinalPrice());

    public void AddItem(ICartItem item){
        _items.Add(item);
        UpdatePrice();
    }
    public void RemoveItem(ICartItem item){
        _items.Remove(item);
        UpdatePrice();
    }
    public double GetFinalPrice() => _pricingStrategyContext.CalculatePrice(this);
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy) => _pricingStrategyContext.SetDiscountStrategy(discountStrategy);
}