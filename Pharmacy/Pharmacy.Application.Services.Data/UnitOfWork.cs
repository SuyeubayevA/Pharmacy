using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Data.Repositories;

namespace Pharmacy.Infrastructure.Data
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly PharmacyDBContext _db;
        private ProductRepository _productRepository;
        private ProductAmountRepository _productAmountRepository;
        private ProductTypeRepository _productTypeRepository;
        private SalesInfoRepository _salesInfoRepository;
        private WarehouseRepository _warehouseRepository;

        public UnitOfWork(PharmacyDBContext options)
        {
            _db = options;
        }

        public ProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_db);
                return _productRepository;
            }
        }
        public ProductAmountRepository ProductAmount
        {
            get
            {
                if (_productAmountRepository == null)
                    _productAmountRepository = new ProductAmountRepository(_db);
                return _productAmountRepository;
            }
        }
        public ProductTypeRepository ProductType
        {
            get
            {
                if (_productTypeRepository == null)
                    _productTypeRepository = new ProductTypeRepository(_db);
                return _productTypeRepository;
            }
        }
        public SalesInfoRepository SalesInfo
        {
            get
            {
                if (_salesInfoRepository == null)
                    _salesInfoRepository = new SalesInfoRepository(_db);
                return _salesInfoRepository;
            }
        }
        public WarehouseRepository Warehouse
        {
            get
            {
                if (_warehouseRepository == null)
                    _warehouseRepository = new WarehouseRepository(_db);
                return _warehouseRepository;
            }
        }

        public async Task<bool> SaveAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
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
