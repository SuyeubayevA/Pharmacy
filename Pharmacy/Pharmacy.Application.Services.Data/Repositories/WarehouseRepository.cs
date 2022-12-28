using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class WarehouseRepository : IPharmRepository<Warehouse, WarehouseDetailsDTO, WarehouseDTO>
    {
        private readonly PharmacyDBContext db;

        public WarehouseRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(Warehouse warehouse)
        {
            db.Add(warehouse);
        }

        public async Task<WarehouseDetailsDTO> GetAsync(int id)
        {
            IQueryable<Warehouse> query = db.Warehouses
                .Include(w => w.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var warehouses = await query.FirstOrDefaultAsync();
            var warehousesDTO = ObjectMapper.Mapper.Map<WarehouseDetailsDTO>(warehouses);

            return warehousesDTO;
        }

        public async Task<WarehouseDetailsDTO> GetAsync(string name)
        {
            IQueryable<Warehouse> query = db.Warehouses
                .Include(w => w.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Name == name);

            var warehouse = await query.FirstOrDefaultAsync();
            var warehouseDTO = ObjectMapper.Mapper.Map<WarehouseDetailsDTO>(warehouse);

            return warehouseDTO;
        }

        public async Task<WarehouseDTO[]> GetAllASync()
        {
            IQueryable<WarehouseDTO> query = from w in db.Warehouses
                                          select new WarehouseDTO
                                          {
                                              Id = w.Id,
                                              Name = w.Name,
                                              Address = w.Address
                                          };

            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(Warehouse warehouse)
        {
            db.Update(warehouse);
        }

        public void Delete(int id)
        {
            var model = db.Find<Warehouse>(id);
            if (model != null) db.Remove(model);
        }
    }
}
