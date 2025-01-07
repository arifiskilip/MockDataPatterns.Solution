using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Infrastructure.Builders;
using MockDataPatterns.Infrastructure.Factories;
using MockDataPatterns.Infrastructure.Strategies;
using Xunit;

namespace MockDataPatterns.Tests.Unit
{
    /// <summary>
    /// Ürün ile ilgili test senaryoları
    /// </summary>
    public class ProductTests
    {
        private readonly IMockDataFactory<Product> _productFactory;
        private readonly ProductBuilder _productBuilder;

        public ProductTests()
        {
            _productFactory = new MockDataFactory<Product>(new RandomProductStrategy());
            _productBuilder = new ProductBuilder();
        }

        [Fact]
        public void Create_WithRandomStrategy_ShouldCreateValidProduct()
        {
            // Act
            var product = _productFactory.Create();

            // Assert
            Assert.NotNull(product);
            Assert.NotEmpty(product.Name);
            Assert.NotEmpty(product.SKU);
            Assert.NotEmpty(product.Description);
            Assert.NotEmpty(product.Categories);
            Assert.True(product.Price.Amount > 0);
        }

        [Fact]
        public void Create_WithDiscountStrategy_ShouldApplyDiscount()
        {
            // Arrange
            var discountPercentage = 20m;
            _productFactory.SetStrategy(new DiscountedProductStrategy(discountPercentage));

            // Act
            var product = _productFactory.Create();

            // Assert
            Assert.Contains("SALE", product.Description);
            Assert.Contains("20% OFF", product.Description);
        }

        [Fact]
        public void Build_WithBuilder_ShouldCreateSpecificProduct()
        {
            // Arrange & Act
            var product = _productBuilder
                .WithBasicInfo(
                    name: "Test Product",
                    description: "Test Description",
                    brand: "Test Brand",
                    price: 99.99m)
                .WithSKU("TEST-123")
                .WithStock(10)
                .WithCategories("Test Category", "Another Category")
                .WithWeight(1.5m)
                .Build();

            // Assert
            Assert.Equal("Test Product", product.Name);
            Assert.Equal("Test Description", product.Description);
            Assert.Equal("Test Brand", product.Brand);
            Assert.Equal(99.99m, product.Price.Amount);
            Assert.Equal("TEST-123", product.SKU);
            Assert.Equal(10, product.StockQuantity);
            Assert.True(product.IsAvailable);
            Assert.Equal(2, product.Categories.Count);
            Assert.Equal(1.5m, product.Weight);
        }
    }
}
