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
        public async Task<bool> Create(Warehouse warehouse)
        {
            db.Add(warehouse);
            return (await db.SaveChangesAsync()) > 0;
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

        public async Task<bool> Update(Warehouse warehouse)
        {
            db.Update(warehouse);
            return (await db.SaveChangesAsync()) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var model = db.Find<Warehouse>(id);
            if (model != null) db.Remove(model);

            return (await db.SaveChangesAsync()) > 0;
        }
    }
}
