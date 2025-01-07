# Mock Data Patterns Project

Bu proje, unit testler için mock veri üretimini tasarım desenleri kullanarak gerçekleştiren bir örnek uygulamadır.

## Proje Yapısı

```
MockDataPatterns.Solution/
├── src/
│   ├── MockDataPatterns.Core/                 # Çekirdek katman
│   ├── MockDataPatterns.Infrastructure/       # Altyapı katmanı
│   └── MockDataPatterns.Tests/               # Test projesi
```

## Kullanılan Tasarım Desenleri

1. **Strategy Pattern**: Farklı veri üretim stratejileri için
2. **Builder Pattern**: Kompleks test nesnelerinin oluşturulması için
3. **Factory Pattern**: Mock nesnelerin merkezi yönetimi için

## Örnek Kullanımlar

### 1. Factory Pattern ile Rastgele Veri Üretimi

```csharp
// Factory oluşturma
var customerFactory = new MockDataFactory<Customer>(new RandomCustomerStrategy());

// Tekil veri üretimi
var customer = customerFactory.Create();

// Çoklu veri üretimi
var customers = customerFactory.CreateMany(10);

// Strateji değiştirme
customerFactory.SetStrategy(new PremiumCustomerStrategy());
var premiumCustomer = customerFactory.Create();
```

### 2. Builder Pattern ile Spesifik Veri Oluşturma

```csharp
// Builder kullanımı
var customer = new CustomerBuilder()
    .WithBasicInfo("John", "Doe", "john.doe@example.com")
    .WithPhone("+11234567890")
    .WithAddress("123 Main St", "New York", "NY", "10001")
    .AsPremiumCustomer()
    .Build();
```

### 3. Kompleks Senaryo Örneği

```csharp
// Factories
var customerFactory = new MockDataFactory<Customer>(new PremiumCustomerStrategy());
var productFactory = new MockDataFactory<Product>(new DiscountedProductStrategy(20));

// Create test data
var customer = customerFactory.Create();
var products = productFactory.CreateMany(3).ToList();

// Build order
var order = new OrderBuilder()
    .WithCustomer(customer.Id)
    .WithOrderNumber($"ORD-{DateTime.Now:yyyyMMdd}-TEST")
    .WithShippingAddress(customer.GetDefaultAddress())
    .WithPaymentMethod(PaymentMethod.CreditCard)
    .WithStatus(OrderStatus.Pending)
    .Build();

// Add items to order
foreach (var product in products)
{
    order.AddItem(new OrderItem
    {
        ProductId = product.Id,
        ProductName = product.Name,
        Quantity = 2,
        UnitPrice = product.Price
    });
}
```

## Projeyi Genişletme

1. Yeni bir Entity için mock data üretimi ekleme:
   - Core katmanında entity tanımlama
   - Infrastructure katmanında strategy implementasyonu
   - Test senaryoları ekleme

2. Yeni bir strateji ekleme:
   ```csharp
   public class CustomStrategy : BaseMockStrategy<Customer>
   {
       public override Customer GenerateSingle()
       {
           // Özel strateji implementasyonu
       }
   }
   ```

3. Yeni bir builder özelliği ekleme:
   ```csharp
   public CustomerBuilder WithCustomProperty(string value)
   {
       _customer.CustomProperty = value;
       return this;
   }
   ```

## Test Coverages

Projedeki test senaryoları şunları kapsamaktadır:

- Temel veri üretimi testleri
- Validasyon testleri
- Kompleks senaryo testleri
- Entegrasyon testleri

## Best Practices

1. Her zaman interface üzerinden çalışın (IMockDataFactory, IMockDataStrategy)
2. Yeni özellikler eklerken validasyonları unutmayın
3. Test coverage'ı yüksek tutun
4. Kod tekrarından kaçının
5. SOLID prensiplerine uygun geliştirme yapın
