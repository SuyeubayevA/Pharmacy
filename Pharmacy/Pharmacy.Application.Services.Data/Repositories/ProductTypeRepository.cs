using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductTypeRepository : IPharmRepository<ProductType>
    {
        private readonly PharmacyDBContext db;

        public ProductTypeRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(ProductType productType)
        {
            db.Add(productType);
        }

        public async Task<ProductType?> GetAsync(int id)
        {
            var query = db.ProductTypes
                .Include(pt => pt.Products)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var productTypes = await query.FirstAsync();

            return productTypes;
        }

        public async Task<ProductType?> GetAsync(string name)
        {
            IQueryable<ProductType> query = db.ProductTypes
                .Include(pt => pt.Products)
                .AsQueryable();
            query = query.Where(x => x.Name == name);

            var productTypes = await query.FirstAsync();

            return productTypes;
        }

        public async Task<IList<ProductType>?> GetAllASync()
        {
            var query = await db.ProductTypes.AsQueryable().ToListAsync();
                                               
            return query;
        }

        public void Update(ProductType productType)
        {
            db.Update(productType);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductType>(id);
            if(model != null) db.Remove(model);
        }
    }
}
