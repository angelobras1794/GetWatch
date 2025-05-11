using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using GetWatch.Services.ShoppingCart;

namespace GetWatch.Tests
{
    public class ShoppingCartTest
    {
    
        [Fact]
        public void AddMultipleItems_ShouldUpdatePriceCorrectly()
        {
            // Arrange
            var cart = new ShoppingCart();
            var item1 = new Product(10,414124,new Guid());
            var item2 = new Product(20, 492523,new Guid());
            // Act
            cart.AddItem(item1);
            cart.AddItem(item2);
            // Assert
            Assert.Equal(30.0, cart.Price);
        }

        [Fact]
        public void AddItem_ShouldUpdateBundlePriceCorrectly()
        {
            // Arrange
            var cart = new ShoppingCart();
            var bundle = new Bundle("Test Bundle");
            bundle.AddProduct(new Product(10,414124,new Guid()));
            bundle.AddProduct(new Product(10,414124,new Guid()));
            // Act
            cart.AddItem(bundle);

            // Assert
            Assert.Equal(20.0, cart.Price);
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_cart()
        {
            // Arrange
            var cart = new ShoppingCart();
            var item = new Product(10,414124,new Guid());
            cart.AddItem(item);
            var discountStrategy = new PercentageDiscount(0.10); // 10% discount
            // Act
            cart.SetDiscountStrategy(discountStrategy);
            // Assert
            Assert.Equal(9, cart.GetFinalPrice());
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_bundle()
        {
            // Arrange
            var bundle = new Bundle("Test Bundle");
            var item1 = new Product(10,414124,new Guid());
            var item2 = new Product(10,414124,new Guid());
            bundle.AddProduct(item1);
            bundle.AddProduct(item2);
            var discountStrategy = new PercentageDiscount(0.20); // 20% discount
            // Act
            bundle.SetDiscountStrategy(discountStrategy);
            // Assert
            Assert.Equal(16, bundle.GetFinalPrice());
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_cart_and_bundle()
        {
            // Arrange
            var discountStrategy = new PercentageDiscount(0.10); // 10% discount
            var cart = new ShoppingCart();
            var bundle = new Bundle("Test Bundle");
            var item1 = new Product(10,414124,new Guid());
            var item2 = new Product(10,414124,new Guid());
            bundle.AddProduct(item1);
            bundle.AddProduct(item2);
            bundle.SetDiscountStrategy(discountStrategy);

            cart.AddItem(bundle);
            cart.SetDiscountStrategy(discountStrategy);
            Assert.Equal(16.2, cart.GetFinalPrice());
        }
    }
}