using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
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

        public async Task<ProductAmount> GetAsync(int id)
        {
            IQueryable<ProductAmount> query = db.ProductAmounts.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstAsync();
        }

        public async Task<ProductAmount[]> GetAllASync()
        {
            IQueryable<ProductAmount> query = db.ProductAmounts.AsQueryable();
            query = query.OrderByDescending(p => p.Product.Name);

            return await query.ToArrayAsync();
        }

        public void Update(ProductAmount productAmount)
        {
            db.Update(productAmount);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductAmount>(id);
            db.Remove(model);
        }
    }
}
