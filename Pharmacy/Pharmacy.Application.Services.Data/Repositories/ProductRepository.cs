using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductRepository : IPharmRepository<Product>
    {
        private readonly PharmacyDBContext db;

        public ProductRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(Product product) 
        {
            db.Add(product);
        }

        public async Task<Product?> GetAsync(int id)
        {
            var query = db.Products
                .Include(pr => pr.ProductType)
                .Include(pr => pr.SalesInfo)
                .Include(pr => pr.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var product = await query.SingleOrDefaultAsync();

            return product;
        }

        public async Task<Product?> GetAsync(string name)
        {
            var query = db.Products
                .Include(pr => pr.ProductType)
                .Include(pr => pr.SalesInfo)
                .Include(pr => pr.ProductAmounts)
                .AsQueryable();

            query = query.Where(x => x.Name == name);

            var product = await query.FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<Product>?> GetAllASync()
        {
            var products = await db.Products.AsQueryable().ToListAsync();

            return products;
        }

        public void Update(Product product)
        {
            db.Update(product);
        }

        public void UpdateWarehouseLink(int productId, int warehouseId, int amount = 0, float discount = 0)
        {
            var wareHouse = db.Warehouses.Where(x => x.Id== warehouseId).FirstOrDefault();
            var product = db.Products.FirstOrDefault(p => p.Id== productId);

            if(wareHouse != null && product != null)
            {
                var productAmount = new ProductAmount
                {
                    Product = product,
                    Warehouse = wareHouse,
                    Amount = amount,
                    Discount = discount
                };

                if(product.ProductAmounts == null)
                {
                    product.ProductAmounts = new List<ProductAmount>
                    {
                        productAmount
                    };
                }
                else
                {
                    product.ProductAmounts.Add(productAmount);
                }
                db.Update(product);
            }

        }

        public void Delete(int id)
        {
            var model = db.Find<Product>(id);
            if(model != null) db.Remove(model);
        }
    }
}
