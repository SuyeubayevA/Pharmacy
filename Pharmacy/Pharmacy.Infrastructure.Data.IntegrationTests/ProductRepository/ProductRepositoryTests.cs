using Azure.Core;
using Pharmacy.Domain.Core;
using Shouldly;

namespace Pharmacy.Infrastructure.Data.IntegrationTests.ProductRepository
{
    public class ProductRepositoryTests: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        public ProductRepositoryTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private static readonly Product testProduct = new Product
        {
            Name= "Panacea",
            Description = "Medicines for all deseases",
            Price = 1000000.0F,
            ProductTypeId = 1,
            SalesInfoId= 1
        };

        [Fact]
        public async Task CreateProduct()
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            bool isSuccesful;

            if (await prodRepo.GetAsync(testProduct.Name) != null)
            {
                isSuccesful = true;
            }
            else
            {
                isSuccesful = await prodRepo.Create(testProduct);
            }

            //Assert
            isSuccesful.ShouldBe(true);
        }

        [Theory]
        [InlineData(4)]
        public async Task GetProductById(int id)
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            var product = await prodRepo.GetAsync(id);

            //Assert
            product?.Id.ShouldBe(id);
            product?.ProductType?.Id.ShouldBe(1);
            product?.ProductAmounts.Any(x=>x.WarehouseId == 1).ShouldBeTrue();
        }

        [Theory]
        [InlineData("Test")]
        public async Task GetProductByName(string name)
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            var product = await prodRepo.GetAsync(name);

            //Assert
            product?.Name.ShouldBe(name);
            product?.ProductType?.Id.ShouldBe(1);
            product?.ProductAmounts.Any(x => x.WarehouseId == 1).ShouldBeTrue();
        }

        [Fact]
        public async Task GetAllProducts()
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            var product = await prodRepo.GetAllASync();

            //Assert
            product?.Count().ShouldBeGreaterThan(0);
            product?.Any(x => x.Name == "Panacea").ShouldBeTrue();
        }

        [Theory]
        [InlineData(150, "Panacea")]
        public async Task updatesTestProductAndSetPrice150(int newPrice, string name)
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            var product = await prodRepo.GetAsync(name);
            product.Price = newPrice;

            var isUpdated = await prodRepo.Update(product);

            //Assert
            isUpdated.ShouldBeTrue();
        }

        [Theory]
        [InlineData("Panacea")]
        public async Task removesTestProduct(string name)
        {
            //Arrange
            _factory.CreateClient();
            var db = _factory.DBContext;
            var prodRepo = new Repositories.ProductRepository(db);

            //Act
            var product = await prodRepo.GetAsync(name);
            //Assert
            product.ShouldNotBeNull();

            await prodRepo.Delete(product.Id);
            product = await prodRepo.GetAsync(name);
            //Assert
            product.ShouldBeNull();
        }
    }
}
