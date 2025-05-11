using GetWatch.Services.ShoppingCart;

namespace GetWatch.Services.ShoppingCart
{
    public class ShoppingCart : IDiscountable, IShoppingCart
    {
        public List<ICartItem> _items { get; set; }
        private PricingStrategyContext _pricingStrategyContext;
        public double Price { get; set; }
        public Guid Id { get; set; }

        public ShoppingCart(double price = 0,Guid id = new Guid())
        {
            _items = new List<ICartItem>();
            _pricingStrategyContext = new PricingStrategyContext(new NoDiscountStrategy());
            Price = price;
            Id = id;
        }
        private void UpdatePrice() => Price = _items.Sum(item => item.GetFinalPrice());

        public List<ICartItem> GetItems() {return _items;}
  

        public void AddItem(ICartItem item)
        {
            _items.Add(item);
            UpdatePrice();
        }
        public void RemoveItem(ICartItem item)
        {
            _items.Remove(item);
            UpdatePrice();
        }
        public double GetFinalPrice() => _pricingStrategyContext.CalculatePrice(this);
        public void SetDiscountStrategy(IDiscountStrategy discountStrategy) => _pricingStrategyContext.SetDiscountStrategy(discountStrategy);

        public void IncreaseQuantity(ICartItem item)
        {
            var cartItem = _items.FirstOrDefault(i => i.Equals(item));
            if (cartItem != null)
            {
                cartItem.Quantity++;
                UpdatePrice();
            }
        }

        public void DecreaseQuantity(ICartItem item)
        {
            var cartItem = _items.FirstOrDefault(i => i.Equals(item));
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    UpdatePrice();
                }
            }
        }
    }
}