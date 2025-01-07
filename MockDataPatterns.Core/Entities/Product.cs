using System;
using System.Collections.Generic;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Core.Entities
{
    /// <summary>
    /// Ürün detaylarını tutan entity
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsAvailable { get; set; }
        public List<string> Categories { get; set; } = new();
        public string Brand { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsInStock() => StockQuantity > 0;
    }
}
