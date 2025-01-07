using System;
using System.Collections.Generic;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.ValueObjects;
using MockDataPatterns.Infrastructure.Strategies.Base;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// Rastgele müşteri verisi üreten strateji
    /// </summary>
    public class RandomCustomerStrategy : BaseMockStrategy<Customer>
    {
        private readonly string[] _firstNames = { "John", "Jane", "Michael", "Sarah", "David", "Emma", "James", "Emily", "Robert", "Maria" };
        private readonly string[] _lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Wilson", "Taylor" };
        private readonly string[] _cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix" };
        private readonly string[] _states = { "NY", "CA", "IL", "TX", "AZ" };

        public override Customer GenerateSingle()
        {
            var firstName = _firstNames[Random.Next(_firstNames.Length)];
            var lastName = _lastNames[Random.Next(_lastNames.Length)];

            return new Customer
            {
                Id = Random.Next(1, 10000),
                FirstName = firstName,
                LastName = lastName,
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}@example.com",
                Phone = new PhoneNumber($"+1{Random.Next(100, 999)}{Random.Next(100, 999)}{Random.Next(1000, 9999)}"),
                Type = (CustomerType)Random.Next(0, 3),
                RegisteredDate = DateTime.Now.AddDays(-Random.Next(365)),
                IsActive = true,
                LoyaltyPoints = Random.Next(0, 1000),
                Addresses = GenerateRandomAddresses()
            };
        }

        private List<Address> GenerateRandomAddresses()
        {
            var addresses = new List<Address>();
            var addressCount = Random.Next(1, 3);

            for (int i = 0; i < addressCount; i++)
            {
                var city = _cities[Random.Next(_cities.Length)];
                var state = _states[Random.Next(_states.Length)];

                addresses.Add(new Address
                {
                    Street = $"{Random.Next(100, 9999)} {_lastNames[Random.Next(_lastNames.Length)]} St",
                    City = city,
                    State = state,
                    PostalCode = Random.Next(10000, 99999).ToString(),
                    Country = "USA",
                    IsDefault = i == 0,
                    AddressType = i == 0 ? "Both" : "Shipping"
                });
            }

            return addresses;
        }
    }
}