using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.DTO;
//using System.Data.Entity;

namespace Pharmacy.Infrastructure.Data.Repositories
{
    public class ProductRepository : IPharmRepository<Product>
    {
        private readonly PharmacyDBContext db;
        private readonly IMapper _mapper;

        public ProductRepository(PharmacyDBContext context, IMapper mapper)
        {
            this.db = context;
            this._mapper = mapper;
        }
        public void Create(Product product)
        {
            db.Add(product);
        }

        public async Task<Product?> GetAsync(int id)
        {
            IQueryable<Product> query = db.Products.AsQueryable();
            query = query.Where(x => x.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product?> GetAsync(string name)
        {
            IQueryable<Product> query = db.Products.AsQueryable();
            query = query.Where(x => x.Name == name);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product[]> GetAllASync()
        {
            IQueryable<Product> query = from p in db.Products
                        select new Product
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            ProductTypeId = p.ProductTypeId,
                            SalesInfoId = p.SalesInfoId,
                            ProductType = new ProductType
                            {
                                Id = p.ProductTypeId,
                                Name = p.ProductType.Name,
                                Properties = p.ProductType.Properties
                            }
                            //,
                            //SalesInfo = new SalesInfo
                            //{
                            //    Id = p.SalesInfo.Id,
                            //    Sales = p.SalesInfo.Sales,
                            //    ProductReminder = p.SalesInfo.ProductReminder,
                            //    CreatedDate = p.SalesInfo.CreatedDate,
                            //    EditDate = p.SalesInfo.EditDate,
                            //    ProductId = p.SalesInfo.ProductId
                            //    ,
                            //    Product = new Product
                            //    {
                            //        Name = p.Name,
                            //        Description = p.Description,
                            //        Price = p.Price
                            //    }
                            //}
                        };

            query = query.OrderByDescending(p => p.Name);

            return await query.ToArrayAsync();
        }

        public void Update(Product product)
        {
            db.Update(product);
        }

        public void Delete(int id)
        {
            var model = db.Find<Product>(id);
            if(model != null) db.Remove(model);
        }
    }
}
