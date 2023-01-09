
using Microsoft.AspNetCore.Mvc.Testing;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Handlers.ProductQueriesHanders;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Models;
using Pharmacy.Queries;
using Shouldly;
using System.Net.Http.Json;

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
            //Arrange
            string url = "/api/Product";
            var inputValue = new ProductModel
            {
                Name = "IntegrtionTestName",
                Description = "IntegrationTestDescription",
                ProductTypeId= 1
            };
            var command = new CreateProductCommand(inputValue);

            //Act
            var postResponse = await _httpClient.PostAsJsonAsync(url, command);

            //Assert
            postResponse.EnsureSuccessStatusCode();
            var insertedProduct = await postResponse.Content.ReadFromJsonAsync<Product>();
            insertedProduct.Description.ShouldBe(inputValue.Description);
        }

        [Fact]
        public async Task GetAllProducts()
        {
            //var prodRepo = new ProductRepository(_mockDbContext);
            var entitiesUow = new UnitOfWork(_mockDbContext);
            var handler = new GetAllProductsHandler(entitiesUow);
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);
            result.ShouldBeOfType<ProductDTO[]>();
        }
    }
}
