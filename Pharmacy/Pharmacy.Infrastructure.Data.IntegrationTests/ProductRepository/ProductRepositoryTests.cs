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

            //_factory.CreateClient();
            _uow = provider.GetService<IUnitOfWork>();
        }


        //static string someText = new Randomizer().Chars(count: 200).ToString();
        //private static readonly Product testProduct = new Faker<Product>()
        //    .RuleFor(x => x.Name, x => x.Person.FullName)
        //    .RuleFor(x => x.Description, x => someText)
        //    .RuleFor(x => x.Price, x => x.Random.Float(0, 10_000))
        //    .RuleFor(x => x.ProductTypeId, x => 1)
        //    .RuleFor(x => x.SalesInfoId, x => 1);

        //private static readonly Product testProduct = new Product()
        //{
        //    Name= "Panacea",
        //    Description = "Medicines for all deseases",
        //    Price = 1000000.0F,
        //    ProductTypeId = 1,
        //    SalesInfoId= 1
        //};

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
            product?.ProductAmounts.Any(x => x.WarehouseId == 1).ShouldBeTrue();
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
            product?.Any(x => x.Name == "test").ShouldBeTrue();
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
