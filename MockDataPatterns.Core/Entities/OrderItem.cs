using MockDataPatterns.Core.ValueObjects;

namespace MockDataPatterns.Core.Entities
{
    /// <summary>
    /// Sipariş içindeki ürün detaylarını tutan entity
    /// </summary>
    public class OrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public Money UnitPrice { get; set; }

        // Sadece debug için
        public decimal GetItemTotal() => UnitPrice.Amount * Quantity;

        public override string ToString()
        {
            return $"{ProductName} x {Quantity} @ {UnitPrice.Amount} = {GetItemTotal()}";
        }
    }
}
