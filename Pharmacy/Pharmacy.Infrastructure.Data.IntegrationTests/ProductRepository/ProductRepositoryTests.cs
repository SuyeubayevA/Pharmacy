using Azure.Core;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.Abstracts;
using Shouldly;

namespace Pharmacy.Infrastructure.Data.IntegrationTests.ProductRepository
{
    public class ProductRepositoryTests: IClassFixture<CustomWebApplicationFactory<Program>>
    {
        //private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly IUnitOfWork _uow;
        public ProductRepositoryTests(IUnitOfWork uow)
        {
            _uow = uow;
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
            var prodRepo = _uow.Product;

            //Act
            bool isSuccesful;

            if (await prodRepo.GetAsync(testProduct.Name) != null)
            {
                isSuccesful = true;
            }
            else
            {
                prodRepo.Create(testProduct);

                await _uow.SaveAsync();

                isSuccesful = true;
            }

            //Assert
            isSuccesful.ShouldBe(true);
        }

        [Theory]
        [InlineData(4)]
        public async Task GetProductById(int id)
        {
            //Arrange
            var prodRepo = _uow.Product;

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
            var prodRepo = _uow.Product;

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
            var prodRepo = _uow.Product;

            //Act
            var product = await prodRepo.GetAllAsync();

            //Assert
            product?.Count().ShouldBeGreaterThan(0);
            product?.Any(x => x.Name == "Panacea").ShouldBeTrue();
        }

        [Theory]
        [InlineData(150, "Panacea")]
        public async Task updatesTestProductAndSetPrice150(int newPrice, string name)
        {
            //Arrange
            var prodRepo = _uow.Product;
            bool isUpdated;

            //Act
            var product = await prodRepo.GetAsync(name);

            if (product != null)
            {
                product.Price = newPrice;

                prodRepo.Update(product);

                await _uow.SaveAsync();

                isUpdated = true;
            } 
            else 
            {
                isUpdated = false;
            }

            //Assert
            isUpdated.ShouldBeTrue();
        }

        [Theory]
        [InlineData("Panacea")]
        public async Task removesTestProduct(string name)
        {
            //Arrange
            var prodRepo = _uow.Product;

            //Act
            var product = await prodRepo.GetAsync(name);
            //Assert
            product.ShouldNotBeNull();

            prodRepo.Delete(product.Id);
            await _uow.SaveAsync();

            product = await prodRepo.GetAsync(name);
            //Assert
            product.ShouldBeNull();
        }
    }
}
