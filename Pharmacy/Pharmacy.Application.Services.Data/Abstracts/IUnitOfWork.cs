using Pharmacy.Infrastructure.Data.Repositories;

namespace Pharmacy.Infrastructure.Data.Abstracts
{
    public interface IUnitOfWork
    {
        ProductRepository Product { get; }
        ProductAmountRepository ProductAmount { get; }
        ProductTypeRepository ProductType { get; }
        SalesInfoRepository SalesInfo { get; }
        WarehouseRepository Warehouse { get; }
        Task<bool> SaveAsync();
    }
}
