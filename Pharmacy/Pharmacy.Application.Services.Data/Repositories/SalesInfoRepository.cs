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
    public class SalesInfoRepository : IPharmRepository<SalesInfo>
    {
        private readonly PharmacyDBContext db;

        public SalesInfoRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(SalesInfo productType)
        {
            db.Add(productType);
        }

        public async Task<SalesInfo> GetAsync(int id)
        {
            IQueryable<SalesInfo> query = db.SalesInfos.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstAsync();
        }

        public async Task<SalesInfo> GetAsync(int productId, int id = 0)
        {
            IQueryable<SalesInfo> query = db.SalesInfos.AsQueryable();
            query = query.Where(x => x.ProductId == productId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<SalesInfo[]> GetAllASync()
        {
            IQueryable<SalesInfo> query = db.SalesInfos.AsQueryable();
            query = query.OrderByDescending(p => p.Product.Name);

            return await query.ToArrayAsync();
        }

        public void Update(SalesInfo salesInfo)
        {
            db.Update(salesInfo);
        }

        public void Delete(int id)
        {
            var model = db.Find<SalesInfo>(id);
            db.Remove(model);
        }
    }
}
