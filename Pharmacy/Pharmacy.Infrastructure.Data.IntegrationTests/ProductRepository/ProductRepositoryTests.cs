using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Data.Repositories;
using Shouldly;

namespace Pharmacy.Infrastructure.Data.IntegrationTests.ProductRepositoryTests
{
    public class ProductRepositoryTests
    {
        public IUnitOfWork _uow;

        public ProductRepositoryTests()
        {
            var services = new ServiceCollection();
            services.AddDbContext<PharmacyDBContext>(
             options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PharmacyPDP;Integrated Security=True;")
            );
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductAmountRepository, ProductAmountRepository>();
            services.AddScoped<ISalesInfoRepository, SalesInfoRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            var provider = services.BuildServiceProvider();

            _uow = provider.GetService<IUnitOfWork>();
        }

        [Fact]
        public async Task CreateProduct()
        {
            var productForTesting = generateNewProduct();
            var productRepository = _uow.Product;
            bool isSuccesful = false;

            if (await productRepository.GetAsync(productForTesting.Name) == null)
            {
                productRepository.Create(productForTesting);
                try
                {
                    await _uow.SaveAsync();
                    isSuccesful = true;
                }
                catch
                {
                    isSuccesful = false;
                }
            }

            var productForChecking = await productRepository.GetAsync(productForTesting.Name);
            isSuccesful.ShouldBe(true);
            productForChecking.ShouldNotBeNull();
            productForChecking.Name.ShouldBeEquivalentTo(productForTesting.Name, "productForChecking and productForTesting have diff Names");
        }

        [Fact]
        public async Task GetProductByName()
        {
            var productForTesting = generateNewProduct();
            var productRepository = _uow.Product;

            productRepository.Create(productForTesting);
            await _uow.SaveAsync();

            var product = await productRepository.GetAsync(productForTesting.Name);

            product?.ShouldNotBeNull();
            product?.ProductTypeId.ShouldBe(1);
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
            product?.Any(x => x.Name == "test").ShouldBeTrue();
        }

        [Theory]
        [InlineData(150)]
        public async Task updatesTestProductAndSetPrice150(int newPrice)
        {
            var productRepository = _uow.Product;
            var productForTesting = generateNewProduct();
            bool isSuccesful = false;
            bool isUpdated;

            if (await productRepository.GetAsync(productForTesting.Name) == null)
            {
                productRepository.Create(productForTesting);
                try
                {
                    await _uow.SaveAsync();
                    isSuccesful = true;
                }
                catch
                {
                    isSuccesful = false;
                }
            }

            var productForChecking = await productRepository.GetAsync(productForTesting.Name);
            isSuccesful.ShouldBe(true);
            productForChecking.ShouldNotBeNull();

            if (productForChecking != null)
            {
                productForChecking.Price = newPrice;
                productRepository.Update(productForChecking);
                await _uow.SaveAsync();
                isUpdated = true;
            }
            else
            {
                isUpdated = false;
            }

            var updatedProduct = await productRepository.GetAsync(productForTesting.Name);
            isUpdated.ShouldBeTrue();
            updatedProduct.Price.ShouldBe(newPrice);
        }

        [Fact]
        public async Task removesTestProduct()
        {
            var productForTesting = generateNewProduct();
            var productRepository = _uow.Product;
            bool isSuccesful = false;

            if (await productRepository.GetAsync(productForTesting.Name) == null)
            {
                productRepository.Create(productForTesting);
                try
                {
                    await _uow.SaveAsync();
                    isSuccesful = true;
                }
                catch
                {
                    isSuccesful = false;
                }
            }

            var productForChecking = await productRepository.GetAsync(productForTesting.Name);
            isSuccesful.ShouldBe(true);
            productForChecking.ShouldNotBeNull();

            productRepository.Delete(productForChecking.Id);
            try
            {
                await _uow.SaveAsync();
                isSuccesful = true;
            }
            catch
            {
                isSuccesful = false;
            }
            isSuccesful.ShouldBe(true);
        }

        private Product generateNewProduct()
        {
            string someText = new Randomizer().Chars(count: 200).ToString();

            return new Faker<Product>()
            .RuleFor(x => x.Name, x => x.Person.FullName)
            .RuleFor(x => x.Description, x => someText)
            .RuleFor(x => x.Price, x => x.Random.Float(0, 10_000))
            .RuleFor(x => x.ProductTypeId, x => 1)
            .RuleFor(x => x.SalesInfoId, x => 1);
    }
    }
}
