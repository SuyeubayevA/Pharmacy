
using Microsoft.AspNetCore.Mvc.Testing;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Repositories;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly PharmacyDBContext _mockDbContext;

        public ProductControllerTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateClient();
            _mockDbContext = MockPharmacyDBContext.GetProductDBContexts().Object;
         }

        public async Task CreateNewProduct()
        {
            ////Arrange
            //var prodRepo = new ProductRepository(_mockDbContext);
            //var inputValue = new Product
            //{
            //    Name = "IntegrtionTestName",
            //    Description = "IntegrationTestDescription",
            //    ProductTypeId = 1
            //};
            ////var command = new CreateProductCommand(inputValue);

            ////Act
            //var list = await prodRepo.GetAllASync();

            ////Assert
            //list?[0].Name.ShouldBe("Product 1");
        }

        
        public async Task GetAllProducts()
        {
            //var prodRepo = new ProductRepository(_mockDbContext);
            //var entitiesUow = new UnitOfWork(_mockDbContext);
            //var handler = new GetAllProductsHandler(entitiesUow);
            //var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);
            //result.ShouldBeOfType<ProductDTO[]>();
        }
    }
}
