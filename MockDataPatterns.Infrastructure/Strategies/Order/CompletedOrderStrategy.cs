using System;
using MockDataPatterns.Core.Entities;
using MockDataPatterns.Core.Enums;

namespace MockDataPatterns.Infrastructure.Strategies
{
    /// <summary>
    /// Tamamlanmış sipariş verisi üreten strateji
    /// </summary>
    public class CompletedOrderStrategy : RandomOrderStrategy
    {
        public override Order GenerateSingle()
        {
            var order = base.GenerateSingle();
            order.Status = OrderStatus.Delivered;
            order.OrderDate = DateTime.Now.AddDays(-Random.Next(7, 30)); // 1-4 hafta önce
            return order;
        }
    }
}
