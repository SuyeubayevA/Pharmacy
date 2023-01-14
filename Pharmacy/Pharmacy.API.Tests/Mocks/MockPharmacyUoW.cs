using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using System.Data.Entity.Infrastructure;

namespace Pharmacy.API.Tests.Mocks
{
    public class MockPharmacyDBContext
    {
        public static Mock<PharmacyDBContext> GetProductDBContexts()
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

            var productsMock = GetQueryableMockDbSet2(products);
            //var productTypesMock = GetQueryableMockDbSet(productTypes);
            //var warehousesMock = GetQueryableMockDbSet(warehouses);

            var dbContext = new Mock<PharmacyDBContext>();
            dbContext.Setup(c => c.Products).Returns(productsMock);
            return dbContext;
        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryableList = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IDbAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(queryableList.GetEnumerator()));

            dbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryableList.Provider));

            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSet.Object;
        }

        private static DbSet<T> GetQueryableMockDbSet2<T>(List<T> sourceList) where T : class
        {
            var queryableList = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new AsyncEnumerator<T>(queryableList.GetEnumerator()));

            dbSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(queryableList.Provider));

            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

            return dbSet.Object;
        }
    }
}
