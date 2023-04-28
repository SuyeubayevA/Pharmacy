using Microsoft.EntityFrameworkCore;
using Moq;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.API.Tests.RepositoriesTests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task GetAllProductsRepoTest_HasProperType_Run_Once()
        {
            var context = new PharmacyDBContext();
            var fakeProducts = Helper.GetFaker<Product>().Generate(5);
            var repoMock = new Mock<ProductRepository>(context);
            repoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(fakeProducts);
            var repo = repoMock.Object;

            var result = await repo.GetAllAsync();
            
            Assert.IsAssignableFrom<IEnumerable<Product>>(result);
            repoMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdProductsRepoTest_Run_Once()
        {
            var context = new PharmacyDBContext();
            var fakeProducts = Helper.GetFaker<Product>().Generate(5);
            var fakeProduct = fakeProducts[0];
            var repoMock = new Mock<ProductRepository>(context);
            repoMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(fakeProduct);

            var repo = repoMock.Object;

            var result = await repo.GetAsync(fakeProduct.Id);

            Assert.IsAssignableFrom<Product>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateProductsRepoTest()
        {
            var fakeProducts = Helper.GetFaker<Product>().Generate(5);
            var queryable = fakeProducts.AsQueryable();
            var dbSet = new Mock<DbSet<Product>>();
            dbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<Product>())).Callback<Product>((s) => fakeProducts.Add(s));
            var dbSetObject = dbSet.Object;
            var context = new Mock<PharmacyDBContext>();
            context.Setup<DbSet<Product>>(x => x.Products).Returns(dbSetObject);
            var fakeProduct = Helper.GetFaker<Product>().Generate();
            var repoMock = new Mock<ProductRepository>(context.Object);

            var repo = repoMock.Object;

            repo.Create(fakeProduct);

            Assert.NotNull(context.Object.Products);
        }
    }
}
