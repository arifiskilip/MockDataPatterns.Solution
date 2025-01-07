using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Core.Entities
{
    /// <summary>
    /// Müşteri bilgilerini tutan ana entity
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PhoneNumber Phone { get; set; }
        public CustomerType Type { get; set; }
        public List<Address> Addresses { get; set; } = new();
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
        public int LoyaltyPoints { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public Address GetDefaultAddress()
        {
            return Addresses.FirstOrDefault(a => a.IsDefault) ?? Addresses.FirstOrDefault();
        }
    }

}
