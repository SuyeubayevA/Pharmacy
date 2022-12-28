using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductAmountRepository : IPharmRepository<ProductAmount, ProductAmountDetailsDTO, ProductAmountDTO>
    {
        private readonly PharmacyDBContext db;

        public ProductAmountRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(ProductAmount productType)
        {
            db.Add(productType);
        }

        public async Task<ProductAmountDetailsDTO> GetAsync(int id)
        {
            IQueryable<ProductAmount> query = db.ProductAmounts
                .Include(w => w.Warehouse)
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var productAmount = await query.FirstOrDefaultAsync();
            var productAmountDetailsDTO = ObjectMapper.Mapper.Map<ProductAmountDetailsDTO>(productAmount);

            return productAmountDetailsDTO;
        }

        public async Task<ProductAmountDetailsDTO> GetAsync(int warehouseId, int ProductId)
        {
            IQueryable<ProductAmount> query = db.ProductAmounts
                .Include(w => w.Warehouse)
                .Include(p => p.Product)
                .AsQueryable();
            query = query.Where(x => x.ProductId == ProductId && x.WarehouseId == warehouseId);

            var productAmount = await query.FirstOrDefaultAsync();
            var productAmountDetailsDTO = ObjectMapper.Mapper.Map<ProductAmountDetailsDTO>(productAmount);

            return productAmountDetailsDTO;
        }

        public async Task<ProductAmountDTO[]> GetAllASync()
        {
            var query = from pa in db.ProductAmounts
                        select new ProductAmountDTO
                        {
                            Id = pa.Id,
                            WarehouseId = pa.WarehouseId,
                            ProductId = pa.ProductId,
                            Amount = pa.Amount,
                            Discount = pa.Discount,
                            ProductName = pa.Product.Name,
                            WarehouseName = pa.Warehouse.Name
                        };

            query = query.OrderByDescending(p => p.ProductName);

            return await query.ToArrayAsync();
        }

        public void Update(ProductAmount productAmount)
        {
            db.Update(productAmount);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductAmount>(id);
            db.Remove(model);
        }
    }
}
