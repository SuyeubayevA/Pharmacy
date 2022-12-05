using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductRepository : IPharmRepository<Product>
    {
        private readonly PharmacyDBContext db;

        public ProductRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(Product product)
        {
            db.Add(product);
        }

        public async Task<Product> GetAsync(int id)
        {
            IQueryable<Product> query = db.Products.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstAsync();
        }

        public async Task<Product> GetAsync(string name)
        {
            IQueryable<Product> query = db.Products.AsQueryable();
            query = query.Where(x => x.Name == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product[]> GetAllASync()
        {
            IQueryable<Product> query = db.Products.AsQueryable();
            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(Product product)
        {
            db.Update(product);
        }

        public void Delete(int id)
        {
            var model = db.Find<Product>(id);
            db.Remove(model);
        }
    }
}
