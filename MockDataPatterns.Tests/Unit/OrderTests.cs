using System;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Infrastructure.Builders;
using MockDataPatterns.Infrastructure.Factories;
using MockDataPatterns.Infrastructure.Strategies;
using Xunit;

namespace MockDataPatterns.Tests.Unit
{
    /// <summary>
    /// Sipariş ile ilgili test senaryoları
    /// </summary>
    public class OrderTests
    {
        private readonly IMockDataFactory<Order> _orderFactory;
        private readonly OrderBuilder _orderBuilder;
        private readonly ProductBuilder _productBuilder;

        public OrderTests()
        {
            _orderFactory = new MockDataFactory<Order>(new RandomOrderStrategy());
            _orderBuilder = new OrderBuilder();
            _productBuilder = new ProductBuilder();
        }

        [Fact]
        public void Create_WithRandomStrategy_ShouldCreateValidOrder()
        {
            // Act
            var order = _orderFactory.Create();

            // Assert
            Assert.NotNull(order);
            Assert.NotEmpty(order.OrderNumber);
            Assert.NotEmpty(order.Items);
            Assert.NotNull(order.ShippingAddress);
            Assert.True(order.TotalAmount.Amount > 0);
        }

        [Fact]
        public void Create_WithCompletedOrderStrategy_ShouldCreateDeliveredOrder()
        {
            // Arrange
            _orderFactory.SetStrategy(new CompletedOrderStrategy());

            // Act
            var order = _orderFactory.Create();

            // Assert
            Assert.Equal(OrderStatus.Delivered, order.Status);
            Assert.True((DateTime.Now - order.OrderDate).TotalDays >= 7);
        }

        [Fact]
        public void Build_WithBuilder_ShouldCreateSpecificOrder()
        {
            // Arrange
            var shippingAddress = new Address
            {
                Street = "123 Main St",
                City = "Test City",
                State = "TS",
                PostalCode = "12345",
                Country = "Test Country",
                IsDefault = true
            };

            var product = _productBuilder
                .WithBasicInfo("Test Product", "Description", "Brand", 100m)
                .WithSKU("TEST-123")
                .WithStock(10)
                .Build();

            // Act
            var order = _orderBuilder
                .WithCustomer(1)
                .WithOrderNumber("TEST-ORD-001")
                .WithStatus(OrderStatus.Processing)
                .WithPaymentMethod(PaymentMethod.CreditCard)
                .WithShippingAddress(shippingAddress)
                .WithBillingAddress(shippingAddress)
                .AddItem(product, 2)
                .WithNotes("Test Order")
                .Build();

            // Assert
            Assert.Equal("TEST-ORD-001", order.OrderNumber);
            Assert.Equal(OrderStatus.Processing, order.Status);
            Assert.Equal(PaymentMethod.CreditCard, order.PaymentMethod);
            Assert.Single(order.Items);
            Assert.Equal(2, order.Items[0].Quantity);
            Assert.Equal(200m, order.TotalAmount.Amount); // 2 * 100
        }

        [Fact]
        public void Build_WithMultipleItems_ShouldCalculateTotalCorrectly()
        {
            // Arrange - Her ürün için yeni bir ProductBuilder instance'ı oluştur
            var product1 = new ProductBuilder()
                .WithBasicInfo("Product 1", "Description", "Brand", 100m)
                .WithSKU("TEST-1")
                .Build();

            var product2 = new ProductBuilder()
                .WithBasicInfo("Product 2", "Description", "Brand", 150m)
                .WithSKU("TEST-2")
                .Build();

            var address = new Address { Street = "Test St", City = "Test City", IsDefault = true };

            // Act
            var order = _orderBuilder
                .WithCustomer(1)
                .WithOrderNumber("TEST-ORD-002")
                .WithShippingAddress(address)
                .AddItem(product1, 2) // 200
                .AddItem(product2, 3) // 450
                .Build();

            // Assert
            Assert.Equal(2, order.Items.Count);
            Assert.Equal(650m, order.TotalAmount.Amount);
        }
    }
}
