using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;
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
        public async Task<bool> Create(SalesInfo salesInfo)
        {
            db.Add(salesInfo);
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<SalesInfo?> GetAsync(int id)
        {
            IQueryable<SalesInfo> query = db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var sailsInfo = await query.FirstOrDefaultAsync();

            return sailsInfo;
        }

        public async Task<SalesInfo?> GetAsync(int productId, int id = 0)
        {
            IQueryable<SalesInfo> query = db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.ProductId == productId);

            var sailsInfo = await query.FirstOrDefaultAsync();

            return sailsInfo;
        }

        public async Task<IEnumerable<SalesInfo>?> GetAllASync()
        {
            var list = await db.SalesInfos.AsQueryable().ToListAsync();

            return list;
        }

        public async Task<bool> Update(SalesInfo salesInfo)
        {
            db.Update(salesInfo);
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var model = db.Find<SalesInfo>(id);
            if (model != null) db.Remove(model);

            return (await db.SaveChangesAsync()) > 0;
        }
    }
}
