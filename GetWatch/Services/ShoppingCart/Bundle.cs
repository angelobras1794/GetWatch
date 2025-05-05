using GetWatch.Services.ShoppingCart;
using System.Collections.Generic;
using System.Linq;

public class Bundle : ICartItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public int Quantity { get; set; } = 1;
    private readonly List<ICartItem> _items = new();
    private PricingStrategyContext _pricingStrategy;

    public Bundle(string title)
    {
        Name = title;
        _pricingStrategy = new PricingStrategyContext(new NoDiscountStrategy());
        Price = 0;
    }

    public void UpdatePrice() => Price = _items.Sum(item => item.Price);
    public void AddProduct(ICartItem item){
        _items.Add(item);
        UpdatePrice();
    }
    public void RemoveProduct(ICartItem item){ 
        _items.Remove(item);
        UpdatePrice();
    }

    public double GetFinalPrice() => _pricingStrategy.CalculatePrice(this) * Quantity;
    public IEnumerable<ICartItem> GetItems() => _items;
    public void SetDiscountStrategy(IDiscountStrategy discountStrategy) => _pricingStrategy.SetDiscountStrategy(discountStrategy);
}