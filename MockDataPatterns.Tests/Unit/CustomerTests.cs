using System;
using System.Linq;
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
    /// Müşteri ile ilgili test senaryoları
    /// </summary>
    public class CustomerTests
    {
        private readonly IMockDataFactory<Customer> _customerFactory;
        private readonly CustomerBuilder _customerBuilder;

        public CustomerTests()
        {
            _customerFactory = new MockDataFactory<Customer>(new RandomCustomerStrategy());
            _customerBuilder = new CustomerBuilder();
        }

        [Fact]
        public void Create_WithRandomStrategy_ShouldCreateValidCustomer()
        {
            // Act
            var customer = _customerFactory.Create();

            // Assert
            Assert.NotNull(customer);
            Assert.NotEmpty(customer.FirstName);
            Assert.NotEmpty(customer.LastName);
            Assert.NotEmpty(customer.Email);
            Assert.NotEmpty(customer.Addresses);

            Assert.True(customer.Addresses.Any(a => a.IsDefault));
        }

        [Fact]
        public void Create_WithPremiumStrategy_ShouldCreatePremiumCustomer()
        {
            // Arrange
            _customerFactory.SetStrategy(new PremiumCustomerStrategy());

            // Act
            var customer = _customerFactory.Create();

            // Assert
            Assert.Equal(CustomerType.Premium, customer.Type);
            Assert.True(customer.LoyaltyPoints >= 1000);
        }

        [Fact]
        public void Build_WithBuilder_ShouldCreateSpecificCustomer()
        {
            // Arrange & Act
            var customer = _customerBuilder
                .WithBasicInfo("John", "Doe", "john.doe@example.com")
                .WithPhone("+11234567890")
                .WithAddress("123 Main St", "New York", "NY", "10001")
                .AsPremiumCustomer(2000)
                .Build();

            // Assert
            Assert.Equal("John", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("john.doe@example.com", customer.Email);
            Assert.Equal("+11234567890", customer.Phone.Value);
            Assert.Equal(CustomerType.Premium, customer.Type);
            Assert.Equal(2000, customer.LoyaltyPoints);
            Assert.Single(customer.Addresses);
            Assert.True(customer.Addresses[0].IsDefault);
        }

        [Fact]
        public void Build_WithoutRequiredFields_ShouldThrowException()
        {
            // Arrange
            var builder = new CustomerBuilder();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => builder.Build());
            Assert.Contains("First name is required", exception.Message);
        }

        [Fact]
        public void CreateMany_WithRandomStrategy_ShouldCreateMultipleUniqueCustomers()
        {
            // Arrange & Act
            var customers = _customerFactory.CreateMany(5).ToList();

            // Assert
            Assert.Equal(5, customers.Count);
            Assert.Equal(5, customers.Select(c => c.Email).Distinct().Count());
            Assert.All(customers, c => Assert.NotEmpty(c.Addresses));
        }
    }
}
