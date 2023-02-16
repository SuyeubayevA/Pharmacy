using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Abstracts;
using System.Data.Entity.Infrastructure;

namespace Pharmacy.API.Tests.Mocks
{
    public class MockPharmacyUoW
    {
        public static Mock<IUnitOfWork> GetUnitOfWorks()
        {
            var products = new List<Product>
            {
                new Product{ 
                    Id = 4, 
                    Name = "Product 1",
                    Description = "Product 1 Description",
                    Price = 250,
                    ProductTypeId= 1,
                    SalesInfoId = 1
                },
                new Product{
                    Id = 7,
                    Name = "Product 2",
                    Description = "Product 2 Description",
                    Price = 1050,
                    ProductTypeId= 1,
                    SalesInfoId = 0
                }
            };
            var productTypes = new List<ProductType>
            {
                new ProductType{ 
                    Id = 1, 
                    Name = "Test",
                    Properties = "Testt"
                }
            };
            var warehouses = new List<Warehouse>
            {
                new Warehouse{
                    Id = 1,
                    Name = "warehouse 1",
                    Address = "far far away"
                }
            };

            var mockUoW = new Mock<IUnitOfWork>();

            var mockProdRepo = new Mock<IProductRepository>();
            mockProdRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);
            mockProdRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return products.SingleOrDefault(p => p.Id == id);
            });
            mockProdRepo.Setup(r => r.Create(It.IsAny<Product>())).Callback<Product>(p =>
            {
                products.Add(p);
            });
            mockProdRepo.Setup(r => r.Update(It.IsAny<Product>())).Callback<Product>(p =>
            {
                var productIndex = products.FindIndex(el => el.Name == p.Name);

                if(productIndex != -1)
                {
                    products[productIndex] = p;
                }
            });
            mockProdRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback<int>(p =>
            {
                var productToBeRemoved = products.FirstOrDefault(el => el.Id == p);

                if(productToBeRemoved != null)
                {
                    products.Remove(productToBeRemoved);
                }
            });

            var mockProdTypeRepo = new Mock<IProductTypeRepository>();
            mockProdTypeRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(productTypes);
            mockProdTypeRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return productTypes.SingleOrDefault(p => p.Id == id);
            });
            mockProdTypeRepo.Setup(r => r.Create(It.IsAny<ProductType>())).Callback<ProductType>(pt =>
            {
                productTypes.Add(pt);
            });
            mockProdTypeRepo.Setup(r => r.Update(It.IsAny<ProductType>())).Callback<ProductType>(pt =>
            {
                var productTypeIndex = productTypes.FindIndex(el => el.Name == pt.Name);

                if (productTypeIndex != -1)
                {
                    productTypes[productTypeIndex] = pt;
                }
            });
            mockProdTypeRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback<int>(pt =>
            {
                var productTypeToBeRemoved = productTypes.FirstOrDefault(el => el.Id == pt);

                if (productTypeToBeRemoved != null)
                {
                    productTypes.Remove(productTypeToBeRemoved);
                }
            });


            var mockWarehouseRepo = new Mock<IWarehouseRepository>();
            mockWarehouseRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(warehouses);
            mockWarehouseRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return warehouses.SingleOrDefault(p => p.Id == id);
            });
            mockWarehouseRepo.Setup(r => r.Create(It.IsAny<Warehouse>())).Callback<Warehouse>(w =>
            {
                warehouses.Add(w);
            });
            mockWarehouseRepo.Setup(r => r.Update(It.IsAny<Warehouse>())).Callback<Warehouse>(w =>
            {
                var warehouseIndex = warehouses.FindIndex(el => el.Name == w.Name);

                if (warehouseIndex != -1)
                {
                    warehouses[warehouseIndex] = w;
                }
            });
            mockWarehouseRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback<int>(id =>
            {
                var warhouseToBeRemoved = warehouses.FirstOrDefault(el => el.Id == id);

                if (warhouseToBeRemoved != null)
                {
                    warehouses.Remove(warhouseToBeRemoved);
                }
            });

            mockUoW.Setup(r => r.Product).Returns(mockProdRepo.Object);
            mockUoW.Setup(r => r.ProductType).Returns(mockProdTypeRepo.Object);
            mockUoW.Setup(r => r.Warehouse).Returns(mockWarehouseRepo.Object);


            return mockUoW;
        }
    }
}
