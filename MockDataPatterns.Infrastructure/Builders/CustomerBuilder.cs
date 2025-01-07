using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Infrastructure.Builders
{
    /// <summary>
    /// Müşteri nesnesi oluşturmak için builder
    /// </summary>
    public class CustomerBuilder : IBuilder<Customer>
    {
        private readonly Customer _customer;
        private bool _hasAddress;

        public CustomerBuilder()
        {
            _customer = new Customer
            {
                RegisteredDate = DateTime.Now,
                IsActive = true,
                Type = CustomerType.Standard
            };
        }

        public CustomerBuilder WithBasicInfo(string firstName, string lastName, string email)
        {
            _customer.FirstName = firstName;
            _customer.LastName = lastName;
            _customer.Email = email;
            return this;
        }

        public CustomerBuilder WithPhone(string phoneNumber)
        {
            _customer.Phone = new PhoneNumber(phoneNumber);
            return this;
        }

        public CustomerBuilder WithAddress(
            string street,
            string city,
            string state,
            string postalCode,
            string country = "USA",
            bool isDefault = true,
            string addressType = "Both")
        {
            var address = new Address
            {
                Street = street,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country,
                IsDefault = isDefault,
                AddressType = addressType
            };

            _customer.Addresses.Add(address);
            _hasAddress = true;
            return this;
        }

        public CustomerBuilder AsPremiumCustomer(int loyaltyPoints = 1000)
        {
            _customer.Type = CustomerType.Premium;
            _customer.LoyaltyPoints = loyaltyPoints;
            return this;
        }

        public CustomerBuilder AsVIPCustomer(int loyaltyPoints = 5000)
        {
            _customer.Type = CustomerType.VIP;
            _customer.LoyaltyPoints = loyaltyPoints;
            return this;
        }

        public Customer Build()
        {
            ValidateCustomer();
            return _customer;
        }

        private void ValidateCustomer()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(_customer.FirstName))
                errors.Add("First name is required");

            if (string.IsNullOrEmpty(_customer.LastName))
                errors.Add("Last name is required");

            if (string.IsNullOrEmpty(_customer.Email))
                errors.Add("Email is required");

            if (!_hasAddress)
                errors.Add("At least one address is required");

            if (errors.Any())
                throw new InvalidOperationException(
                    $"Customer validation failed: {string.Join(", ", errors)}");
        }
    }
}
