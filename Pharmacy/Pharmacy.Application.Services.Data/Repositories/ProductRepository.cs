using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductRepository : IPharmRepository<Product, ProductDetailDTO, ProductDTO>
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

        public async Task<ProductDetailDTO> GetAsync(int id)
        {
            var query = db.Products
                .Include(pr => pr.ProductType)
                .Include(pr => pr.SalesInfo)
                .Include(pr => pr.ProductAmounts)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var product = await query.FirstOrDefaultAsync();

            var productDetailsDto = ObjectMapper.Mapper.Map<ProductDetailDTO>(product);

            return productDetailsDto;
        }

        public async Task<ProductDetailDTO?> GetAsync(string name)
        {
            var query = db.Products
                .Include(pr => pr.ProductType)
                .Include(pr => pr.SalesInfo)
                .Include(pr => pr.ProductAmounts)
                .AsQueryable();

            query = query.Where(x => x.Name == name);

            var product = await query.FirstOrDefaultAsync();
            var productDetailsDto = ObjectMapper.Mapper.Map<ProductDetailDTO>(product);

            return productDetailsDto;
        }

        public async Task<ProductDTO[]> GetAllASync()
        {
            IQueryable<ProductDTO> query = from p in db.Products
                        select new ProductDTO
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            ProductTypeId = p.ProductTypeId,
                            SalesInfoId = p.SalesInfoId
                        };

            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(Product product)
        {
            db.Update(product);
        }

        public bool UpdateWarehouseLink(int productId, int warehouseId, int amount = 0, float discount = 0)
        {
            var wareHouse = db.Warehouses.Where(x => x.Id== warehouseId).FirstOrDefault();
            var product = db.Products.FirstOrDefault(p => p.Id== productId);

            //var product = ObjectMapper.Mapper.Map<Product>(productDTO);
            try
            {
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
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(int id)
        {
            var model = db.Find<Product>(id);
            if(model != null) db.Remove(model);
        }
    }
}
