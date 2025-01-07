using System;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.Interfaces;
using MockDataPatterns.Infrastructure.Strategies.Base;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// Rastgele sipariş verisi üreten strateji
    /// </summary>
    public class RandomOrderStrategy : BaseMockStrategy<Order>
    {
        private readonly IMockDataStrategy<Product> _productStrategy;
        private readonly IMockDataStrategy<Customer> _customerStrategy;

        public RandomOrderStrategy(
            IMockDataStrategy<Product> productStrategy = null,
            IMockDataStrategy<Customer> customerStrategy = null)
        {
            _productStrategy = productStrategy ?? new RandomProductStrategy();
            _customerStrategy = customerStrategy ?? new RandomCustomerStrategy();
        }

        public override Order GenerateSingle()
        {
            var customer = _customerStrategy.GenerateSingle();
            var customerAddress = customer.GetDefaultAddress();

            var order = new Order
            {
                Id = Random.Next(1, 10000),
                OrderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{Random.Next(1000, 9999)}",
                CustomerId = customer.Id,
                Status = (OrderStatus)Random.Next(0, 5),
                PaymentMethod = (PaymentMethod)Random.Next(0, 4),
                OrderDate = DateTime.Now.AddDays(-Random.Next(30)),
                ShippingAddress = customerAddress,
                BillingAddress = customerAddress,
                Notes = "Generated test order"
            };

            // Add random items to order
            var itemCount = Random.Next(1, 5);
            for (int i = 0; i < itemCount; i++)
            {
                var product = _productStrategy.GenerateSingle();
                var quantity = Random.Next(1, 5);

                order.AddItem(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            return order;
        }
    }
}
