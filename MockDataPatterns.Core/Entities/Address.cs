namespace MockDataPatterns.Core.Entities
{
    /// <summary>
    /// Adres bilgilerini tutan entity
    /// </summary>
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
        public string AddressType { get; set; } // Billing, Shipping, etc.

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {PostalCode}, {Country}";
        }
    }
}
