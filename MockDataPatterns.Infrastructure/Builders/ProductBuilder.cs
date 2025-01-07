using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Infrastructure.Builders
{
    /// <summary>
    /// Ürün nesnesi oluşturmak için builder
    /// </summary>
    public class ProductBuilder : IBuilder<Product>
    {
        private readonly Product _product;

        public ProductBuilder()
        {
            _product = new Product
            {
                CreatedDate = DateTime.Now,
                IsAvailable = true,
                Categories = new List<string>()
            };
        }

        public ProductBuilder WithBasicInfo(
            string name,
            string description,
            string brand,
            decimal price)
        {
            _product.Name = name;
            _product.Description = description;
            _product.Brand = brand;
            _product.Price = new Money(price);
            return this;
        }

        public ProductBuilder WithSKU(string sku)
        {
            _product.SKU = sku;
            return this;
        }

        public ProductBuilder WithStock(int quantity)
        {
            _product.StockQuantity = quantity;
            _product.IsAvailable = quantity > 0;
            return this;
        }

        public ProductBuilder WithCategories(params string[] categories)
        {
            _product.Categories.AddRange(categories);
            return this;
        }

        public ProductBuilder WithWeight(decimal weight, string unit = "kg")
        {
            _product.Weight = weight;
            _product.WeightUnit = unit;
            return this;
        }

        public Product Build()
        {
            ValidateProduct();
            return new Product
            {
                Id = _product.Id,
                Name = _product.Name,
                SKU = _product.SKU,
                Description = _product.Description,
                Price = new Money(_product.Price.Amount, _product.Price.Currency),
                Brand = _product.Brand,
                Categories = new List<string>(_product.Categories),
                CreatedDate = _product.CreatedDate,
                IsAvailable = _product.IsAvailable,
                StockQuantity = _product.StockQuantity,
                Weight = _product.Weight,
                WeightUnit = _product.WeightUnit
            };
        }

        private void ValidateProduct()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(_product.Name))
                errors.Add("Name is required");

            if (string.IsNullOrEmpty(_product.SKU))
                errors.Add("SKU is required");

            if (_product.Price?.Amount <= 0)
                errors.Add("Price must be greater than zero");

            if (errors.Any())
                throw new InvalidOperationException(
                    $"Product validation failed: {string.Join(", ", errors)}");
        }
    }

}
