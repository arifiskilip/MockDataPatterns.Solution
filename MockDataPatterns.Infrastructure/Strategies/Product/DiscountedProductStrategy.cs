using System;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// İndirimli ürün verisi üreten strateji
    /// </summary>
    public class DiscountedProductStrategy : RandomProductStrategy
    {
        private readonly decimal _discountPercentage;

        public DiscountedProductStrategy(decimal discountPercentage = 20)
        {
            if (discountPercentage <= 0 || discountPercentage >= 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100");

            _discountPercentage = discountPercentage;
        }

        public override Product GenerateSingle()
        {
            var product = base.GenerateSingle();
            var originalPrice = product.Price.Amount;
            var discountedPrice = originalPrice * (1 - (_discountPercentage / 100));
            product.Price = new Money(Math.Round(discountedPrice, 2));
            product.Description = $"SALE: {product.Description} ({_discountPercentage}% OFF)";
            return product;
        }
    }
}
