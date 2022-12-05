using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<ProductType> GetAsync(int id)
        {
            IQueryable<ProductType> query = db.ProductTypes.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstAsync();
        }

        public async Task<ProductType[]> GetAllASync()
        {
            IQueryable<ProductType> query = db.ProductTypes.AsQueryable();
            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(ProductType productType)
        {
            db.Update(productType);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductType>(id);
            db.Remove(model);
        }
    }
}
