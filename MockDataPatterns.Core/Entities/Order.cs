using System;
using System.Collections.Generic;
using System.Linq;
using MockDataPatterns.Core.Enums;
using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Core.Entities
{
    /// <summary>
    /// Sipariş bilgilerini tutan ana entity
    /// </summary>
    public class Order
    {
        private readonly List<OrderItem> _items = new();

        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public IReadOnlyList<OrderItem> Items => _items;
        public Money TotalAmount { get; private set; } = new Money(0);
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public string Notes { get; set; }

        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.Quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            if (item.UnitPrice?.Amount <= 0)
                throw new ArgumentException("Unit price must be greater than zero");

            _items.Add(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            if (!_items.Any())
            {
                TotalAmount = new Money(0);
                return;
            }

            var total = _items.Sum(item => item.UnitPrice.Amount * item.Quantity);
            var currency = _items.First().UnitPrice.Currency;

            TotalAmount = new Money(total, currency);
        }
    }
}
