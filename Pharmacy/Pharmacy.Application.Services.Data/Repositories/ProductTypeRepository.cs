using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductTypeRepository : IPharmRepository<ProductType, ProductTypeDetailsDTO, ProductTypeDTO>
    {
        private readonly PharmacyDBContext db;

        public ProductTypeRepository(PharmacyDBContext context)
        {
            this.db = context;
        }
        public void Create(ProductType productType)
        {
            db.Add(productType);
        }

        public async Task<ProductTypeDetailsDTO> GetAsync(int id)
        {
            var query = db.ProductTypes
                .Include(pt => pt.Products)
                .AsQueryable();
            query = query.Where(x => x.Id == id);

            var productTypes = await query.FirstAsync();
            var productTypeDetailsDTO = ObjectMapper.Mapper.Map<ProductTypeDetailsDTO>(productTypes);

            return productTypeDetailsDTO;
        }

        public async Task<ProductTypeDetailsDTO> GetAsync(string name)
        {
            IQueryable<ProductType> query = db.ProductTypes
                .Include(pt => pt.Products)
                .AsQueryable();
            query = query.Where(x => x.Name == name);

            var productTypes = await query.FirstAsync();
            var productTypeDetailsDTO = ObjectMapper.Mapper.Map<ProductTypeDetailsDTO>(productTypes);

            return productTypeDetailsDTO;
        }

        public async Task<ProductTypeDTO[]> GetAllASync()
        {
            IQueryable<ProductTypeDTO> query = from pt in db.ProductTypes
                                               select new ProductTypeDTO
                                               {
                                                   Id = pt.Id,
                                                   Name = pt.Name,
                                                   Properties = pt.Properties
                                               };
            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(ProductType productType)
        {
            db.Update(productType);
        }

        public void Delete(int id)
        {
            var model = db.Find<ProductType>(id);
            if(model != null) db.Remove(model);
        }
    }
}
