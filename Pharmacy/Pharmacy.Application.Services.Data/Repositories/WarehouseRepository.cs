using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class WarehouseRepository : IPharmRepository<Warehouse>
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

        public async Task<Warehouse?> GetAsync(int id)
        {
            IQueryable<Warehouse> query = db.Warehouses
                .Include(w => w.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var warehouse = await query.SingleOrDefaultAsync();

            return warehouse;
        }

        public async Task<Warehouse?> GetAsync(string name)
        {
            IQueryable<Warehouse> query = db.Warehouses
                .Include(w => w.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Name == name);

            var warehouse = await query.SingleOrDefaultAsync();

            return warehouse;
        }

        public async Task<IEnumerable<Warehouse>?> GetAllASync()
        {
            var list = await db.Warehouses.AsQueryable().ToListAsync();

            return list;
        }

        public void Update(Warehouse warehouse)
        {
            db.Update(warehouse);
        }

        public void Delete(int id)
        {
            var model = db.Find<Warehouse>(id);
            db.Remove(model);
        }
    }
}
