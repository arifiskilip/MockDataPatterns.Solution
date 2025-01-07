using System;

namespace MockDataPatterns.Core.ValueObjects
{
    /// <summary>
    /// Para birimi ve miktar bilgisini tutan value object
    /// </summary>
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public Money Add(Money other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Currency != Currency)
                throw new ArgumentException("Cannot add different currencies");

            return new Money(Amount + other.Amount, Currency);
        }

        public override string ToString() => $"{Amount} {Currency}";
    }
}
