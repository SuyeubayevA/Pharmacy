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
    public class SalesInfoRepository : IPharmRepository<SalesInfo, SalesInfoDetailsDTO, SalesInfoDTO>
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

        public async Task<SalesInfoDetailsDTO> GetAsync(int id)
        {
            IQueryable<SalesInfo> query = db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var sailsInfo = await query.FirstOrDefaultAsync();
            var sailsInfoDto = ObjectMapper.Mapper.Map<SalesInfoDetailsDTO>(sailsInfo);

            return sailsInfoDto;
        }

        public async Task<SalesInfoDetailsDTO> GetAsync(int productId, int id = 0)
        {
            IQueryable<SalesInfo> query = db.SalesInfos
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.ProductId == productId);

            var sailsInfo = await query.FirstOrDefaultAsync();
            var sailsInfoDto = ObjectMapper.Mapper.Map<SalesInfoDetailsDTO>(sailsInfo);

            return sailsInfoDto;
        }

        public async Task<SalesInfoDTO[]> GetAllASync()
        {
            IQueryable<SalesInfoDTO> query = from si in db.SalesInfos
                                          select new SalesInfoDTO
                                          {
                                              Sales = si.Sales,
                                              ProductReminder = si.ProductReminder,
                                              CreatedDate = si.CreatedDate,
                                              EditDate = si.EditDate
                                          };

            query = query.OrderByDescending(p => p.CreatedDate);

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
