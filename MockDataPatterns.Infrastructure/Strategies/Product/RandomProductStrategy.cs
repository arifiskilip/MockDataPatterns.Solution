using System;
using System.Linq;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.ValueObjects;
using MockDataPatterns.Infrastructure.Strategies.Base;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// Rastgele ürün verisi üreten strateji
    /// </summary>
    public class RandomProductStrategy : BaseMockStrategy<Product>
    {
        private readonly string[] _productNames = { "Laptop", "Smartphone", "Tablet", "Headphones", "Smart Watch" };
        private readonly string[] _brands = { "Apple", "Samsung", "Sony", "Dell", "HP" };
        private readonly string[] _categories = { "Electronics", "Computers", "Accessories", "Mobile Devices" };

        public override Product GenerateSingle()
        {
            var name = _productNames[Random.Next(_productNames.Length)];
            var brand = _brands[Random.Next(_brands.Length)];

            return new Product
            {
                Id = Random.Next(1, 10000),
                Name = $"{brand} {name}",
                SKU = $"SKU{Random.Next(10000, 99999)}",
                Description = $"High quality {name.ToLower()} from {brand}",
                Price = new Money(Random.Next(100, 2000) + Math.Round((decimal)Random.NextDouble(), 2)),
                StockQuantity = Random.Next(0, 100),
                IsAvailable = true,
                Categories = _categories.OrderBy(x => Random.Next()).Take(Random.Next(1, 3)).ToList(),
                Brand = brand,
                Weight = Random.Next(1, 5) + Math.Round((decimal)Random.NextDouble(), 2),
                WeightUnit = "kg",
                CreatedDate = DateTime.Now.AddDays(-Random.Next(90))
            };
        }
    }
}
