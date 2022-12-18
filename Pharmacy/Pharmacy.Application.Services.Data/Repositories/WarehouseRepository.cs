using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;

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

        public async Task<Warehouse> GetAsync(int id)
        {
            IQueryable<Warehouse> query = db.Warehouses.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstAsync();
        }

        public async Task<Warehouse> GetAsync(string name)
        {
            IQueryable<Warehouse> query = db.Warehouses.AsQueryable();
            query = query.Where(x => x.Name == name);

            return await query.FirstAsync();
        }

        public async Task<Warehouse[]> GetAllASync()
        {
            IQueryable<Warehouse> query = db.Warehouses.AsQueryable();
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
            db.Remove(model);
        }
    }
}
