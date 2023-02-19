using Bogus;
using Pharmacy.Domain.Core;

namespace Pharmacy.API.Tests.Helpers
{
    public enum EntityType
    {
        Product,
        ProductType,
        ProductAmount,
        SalesInfo,
        Warehouse
    }
    public static class Factory
    {
        static int ProductId = 0;
        static int ProductAmountId = 0;
        static int ProductTypeId = 0;
        static int SalesInfoId = 0;
        static int WarehouseId = 0;
        public static IEntity CreateItem(EntityType type)
        {
            string someText = new Randomizer().Chars(count: 200).ToString();

            switch (type)
            {
                case EntityType.Product:
                    return (IEntity)new Faker<Product>()
                    .RuleFor(x => x.Id, ProductId++)
                    .RuleFor(x => x.Name, Guid.NewGuid().ToString)
                    .RuleFor(x => x.Description, x => someText)
                    .RuleFor(x => x.Price, x => x.Random.Float(0, 10_000))
                    .RuleFor(x => x.ProductTypeId, x => 1)
                    .RuleFor(x => x.SalesInfoId, x => 1);
                case EntityType.ProductType:
                    return (IEntity)new Faker<ProductType>()
                    .RuleFor(x => x.Id, ProductTypeId++)
                    .RuleFor(x => x.Name, Guid.NewGuid().ToString)
                    .RuleFor(x => x.Properties, x => someText); 
                case EntityType.SalesInfo:
                    return (IEntity)new Faker<SalesInfo>()
                    .RuleFor(x => x.Id, SalesInfoId++)
                    .RuleFor(x => x.Sales, f => f.Random.Int(0, 100))
                    .RuleFor(x => x.ProductReminder, f => f.Random.Int(0, 10_000))
                    .RuleFor(x => x.CreatedDate, DateTime.Now)
                    .RuleFor(x => x.CreatedDate, DateTime.Now);
                case EntityType.ProductAmount :
                    return (IEntity)new Faker<ProductAmount>()
                    .RuleFor(x => x.Id, f => ProductAmountId++)
                    .RuleFor(x => x.WarehouseId, f => f.Random.Int(0, 100))
                    .RuleFor(x => x.ProductId, f => f.Random.Int(0, 100))
                    .RuleFor(x => x.Amount, f => f.Random.Int(0, 10_000))
                    .RuleFor(x => x.Discount, f => f.Random.Float(0, 100));
                default:
                    return (IEntity)new Faker<Warehouse>()
                    .RuleFor(x => x.Id, WarehouseId++)
                    .RuleFor(x => x.Name, f => f.Name.FirstName())
                    .RuleFor(x => x.Address, f => f.Address.FullAddress()); 
            }
        }
    }
}
