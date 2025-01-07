using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// Premium müşteri verisi üreten strateji
    /// </summary>
    public class PremiumCustomerStrategy : RandomCustomerStrategy
    {
        public override Customer GenerateSingle()
        {
            var customer = base.GenerateSingle();
            customer.Type = CustomerType.Premium;
            customer.LoyaltyPoints = Random.Next(1000, 5000); // Premium müşteriler için daha yüksek sadakat puanı
            return customer;
        }
    }
}
