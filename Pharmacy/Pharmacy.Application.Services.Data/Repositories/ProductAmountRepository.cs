using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductAmountRepository : IPharmRepository<ProductAmount>
    {
        private readonly PharmacyDBContext db;

        public ProductAmountRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(ProductAmount productType)
        {
            db.Add(productType);
        }

        public async Task<ProductAmount?> GetAsync(int id)
        {
            IQueryable<ProductAmount> query = db.ProductAmounts
                .Include(w => w.Warehouse)
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var productAmount = await query.SingleOrDefaultAsync();

            return productAmount;
        }

        public async Task<ProductAmount?> GetAsync(int warehouseId, int ProductId)
        {
            IQueryable<ProductAmount> query = db.ProductAmounts
                .Include(w => w.Warehouse)
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.ProductId == ProductId && x.WarehouseId == warehouseId);

            var productAmount = await query.FirstOrDefaultAsync();

            return productAmount;
        }

        public async Task<IList<ProductAmount>?> GetAllASync()
        {
            var productAmounts = await db.ProductAmounts.AsQueryable().ToListAsync();

            return productAmounts;
        }

        public void Update(ProductAmount productAmount)
        {
            db.Update(productAmount);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductAmount>(id);
            if (model != null)
                db.Remove(model);
        }
    }
}
