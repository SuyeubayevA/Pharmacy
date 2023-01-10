using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.Repositories;

namespace Pharmacy.Infrastructure.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWorkMarker
    {
        private readonly PharmacyDBContext db;
        private ProductRepository productRepository;
        private ProductAmountRepository productAmountRepository;
        private ProductTypeRepository productTypeRepository;
        private SalesInfoRepository salesInfoRepository;
        private WarehouseRepository warehouseRepository;

        public UnitOfWork(PharmacyDBContext options)
        {
            db = options;
        }

        public ProductRepository Product
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }
        public ProductAmountRepository ProductAmount
        {
            get
            {
                if (productAmountRepository == null)
                    productAmountRepository = new ProductAmountRepository(db);
                return productAmountRepository;
            }
        }
        public ProductTypeRepository ProductType
        {
            get
            {
                if (productTypeRepository == null)
                    productTypeRepository = new ProductTypeRepository(db);
                return productTypeRepository;
            }
        }
        public SalesInfoRepository SalesInfo
        {
            get
            {
                if (salesInfoRepository == null)
                    salesInfoRepository = new SalesInfoRepository(db);
                return salesInfoRepository;
            }
        }
        public WarehouseRepository Warehouse
        {
            get
            {
                if (warehouseRepository == null)
                    warehouseRepository = new WarehouseRepository(db);
                return warehouseRepository;
            }
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) > 0;
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
