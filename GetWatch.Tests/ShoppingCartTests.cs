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
            var item1 = new Product(1, "Test Product 1", 10.0);
            var item2 = new Product(2, "Test Product 2", 20.0);

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
            bundle.AddProduct(new Product(1, "Test Product 1", 10.0));
            bundle.AddProduct(new Product(2, "Test Product 2", 20.0));
            // Act
            cart.AddItem(bundle);

            // Assert
            Assert.Equal(30.0, cart.Price);
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_cart()
        {
            // Arrange
            var cart = new ShoppingCart();
            var item = new Product(1, "Test Product", 100.0);
            cart.AddItem(item);
            var discountStrategy = new PercentageDiscount(0.10); // 10% discount
            // Act
            cart.SetDiscountStrategy(discountStrategy);
            // Assert
            Assert.Equal(90.0, cart.GetFinalPrice());
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_bundle()
        {
            // Arrange
            var bundle = new Bundle("Test Bundle");
            var item1 = new Product(1, "Test Product 1", 50.0);
            var item2 = new Product(2, "Test Product 2", 50.0);
            bundle.AddProduct(item1);
            bundle.AddProduct(item2);
            var discountStrategy = new PercentageDiscount(0.20); // 20% discount
            // Act
            bundle.SetDiscountStrategy(discountStrategy);
            // Assert
            Assert.Equal(80.0, bundle.GetFinalPrice());
        }

        [Fact]
        public void ApplyDiscount_ShouldUpdatePriceCorrectly_cart_and_bundle()
        {
            // Arrange
            var discountStrategy = new PercentageDiscount(0.10); // 10% discount
            var cart = new ShoppingCart();
            var bundle = new Bundle("Test Bundle");
            var item1 = new Product(1, "Test Product 1", 50.0);
            var item2 = new Product(2, "Test Product 2", 50.0);
            bundle.AddProduct(item1);
            bundle.AddProduct(item2);
            bundle.SetDiscountStrategy(discountStrategy);

            cart.AddItem(bundle);
            cart.SetDiscountStrategy(discountStrategy);
            Assert.Equal(81.0, cart.GetFinalPrice());
        }
    }
}