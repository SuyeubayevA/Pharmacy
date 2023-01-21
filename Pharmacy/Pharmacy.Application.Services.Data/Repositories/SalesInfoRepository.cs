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
        private readonly PharmacyDBContext _db;

        public SalesInfoRepository(PharmacyDBContext context)
        {
            this._db = context;
        }
        public void Create(SalesInfo salesInfo)
        {
            _db.Add(salesInfo);
        }

        public async Task<SalesInfo?> GetAsync(int id)
        {
            IQueryable<SalesInfo> query = _db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var sailsInfo = await query.FirstOrDefaultAsync();

            return sailsInfo;
        }

        public async Task<SalesInfo?> GetAsync(int productId, int id = 0)
        {
            IQueryable<SalesInfo> query = _db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.ProductId == productId);

            var sailsInfo = await query.FirstOrDefaultAsync();

            return sailsInfo;
        }

        public async Task<IEnumerable<SalesInfo>?> GetAllASync()
        {
            var list = await _db.SalesInfos.AsQueryable().ToListAsync();

            return list;
        }

        public void Update(SalesInfo salesInfo)
        {
            _db.Update(salesInfo);
        }

        public void Delete(int id)
        {
            var model = _db.Find<SalesInfo>(id);
            if (model != null) _db.Remove(model);
        }
    }
}
