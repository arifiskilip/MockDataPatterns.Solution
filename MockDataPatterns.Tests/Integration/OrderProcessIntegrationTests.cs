using System;
using System.Linq;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Infrastructure.Builders;
using MockDataPatterns.Infrastructure.Factories;
using MockDataPatterns.Infrastructure.Strategies;
using Xunit;

namespace MockDataPatterns.Tests.Integration
{
    /// <summary>
    /// Entegrasyon test örnekleri
    /// </summary>
    public class OrderProcessIntegrationTests
    {
        private readonly IMockDataFactory<Customer> _customerFactory;
        private readonly IMockDataFactory<Product> _productFactory;
        private readonly OrderBuilder _orderBuilder;

        public OrderProcessIntegrationTests()
        {
            _customerFactory = new MockDataFactory<Customer>(new PremiumCustomerStrategy());
            _productFactory = new MockDataFactory<Product>(new DiscountedProductStrategy(20));
            _orderBuilder = new OrderBuilder();
        }

        [Fact]
        public void CreateOrder_WithPremiumCustomerAndDiscountedProducts_ShouldCalculateCorrectly()
        {
            // Arrange
            var customer = _customerFactory.Create();
            var products = _productFactory.CreateMany(3).ToList();
            var builder = _orderBuilder
                .WithCustomer(customer.Id)
                .WithOrderNumber($"ORD-{DateTime.Now:yyyyMMdd}-TEST")
                .WithShippingAddress(customer.GetDefaultAddress())
                .WithPaymentMethod(PaymentMethod.CreditCard)
                .WithStatus(OrderStatus.Pending);

            // Önce ürünleri ekle
            foreach (var product in products)
            {
                builder.AddItem(product, 2);
            }

            // Sonra build et
            var order = builder.Build();

            // Assert
            Assert.Equal(customer.Id, order.CustomerId);
            Assert.Equal(3, order.Items.Count);
            Assert.All(order.Items, item => Assert.Equal(2, item.Quantity));
            Assert.True(order.TotalAmount.Amount > 0);
        }
    }
}
