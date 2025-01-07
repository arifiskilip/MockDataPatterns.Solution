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
    /// Sipariş nesnesi oluşturmak için builder
    /// </summary>
    public class OrderBuilder : IBuilder<Order>
    {
        private readonly Order _order;
        private bool _hasItems;

        public OrderBuilder()
        {
            _order = new Order
            {
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
            };
        }

        public OrderBuilder WithCustomer(int customerId)
        {
            _order.CustomerId = customerId;
            return this;
        }

        public OrderBuilder WithOrderNumber(string orderNumber)
        {
            _order.OrderNumber = orderNumber;
            return this;
        }

        public OrderBuilder WithStatus(OrderStatus status)
        {
            _order.Status = status;
            return this;
        }

        public OrderBuilder WithPaymentMethod(PaymentMethod paymentMethod)
        {
            _order.PaymentMethod = paymentMethod;
            return this;
        }

        public OrderBuilder WithShippingAddress(Address address)
        {
            _order.ShippingAddress = address;
            return this;
        }

        public OrderBuilder WithBillingAddress(Address address)
        {
            _order.BillingAddress = address;
            return this;
        }

        public OrderBuilder AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero");

            // Her seferinde yeni instance'lar oluştur
            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = quantity,
                UnitPrice = new Money(product.Price.Amount, product.Price.Currency)
            };

            _order.AddItem(orderItem);
            _hasItems = true;

            // Debug için yazdır
            Console.WriteLine($"Added item: {orderItem.ProductName}, " +
                            $"Quantity: {orderItem.Quantity}, " +
                            $"Unit Price: {orderItem.UnitPrice.Amount}, " +
                            $"Total: {orderItem.Quantity * orderItem.UnitPrice.Amount}");

            return this;
        }

        public OrderBuilder WithNotes(string notes)
        {
            _order.Notes = notes;
            return this;
        }

        public Order Build()
        {
            ValidateOrder();
            return _order;
        }

        private void ValidateOrder()
        {
            var errors = new List<string>();

            if (_order.CustomerId <= 0)
                errors.Add("Customer ID is required");

            if (string.IsNullOrEmpty(_order.OrderNumber))
                errors.Add("Order number is required");

            if (_order.ShippingAddress == null)
                errors.Add("Shipping address is required");

            if (!_hasItems)
                errors.Add("Order must contain at least one item");

            if (errors.Any())
                throw new InvalidOperationException(
                    $"Order validation failed: {string.Join(", ", errors)}");
        }
    }
}
