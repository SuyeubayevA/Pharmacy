
using AutoMapper;
using Moq;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Product;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Models;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductCommandsHandlerTests
    {
        private readonly IMapper _mapper;

        public ProductCommandsHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PharmacyMappingProfile());
                cfg.AddProfile(new PharmacyModelsToDTOMappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]

        public async Task CreateProductHandlerTest()
        {
            var fakeProduct = Helper.GetFaker<Product>().Generate(1);
            var newProductModel = _mapper.Map<ProductModel>(fakeProduct.First());
            var fakeUOW = MockPharmacyUoW.GetUnitOfWorks().Object;

            var handler = new CreateProductHandler(fakeUOW, _mapper);
            await handler.Handle(new CreateProductCommand(newProductModel), CancellationToken.None);

            await fakeUOW.Product.GetAsync(fakeProduct.First().Id).ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteProductHandlerTest()
        {
            var fakeUOW = MockPharmacyUoW.GetUnitOfWorks().Object;
            var handler = new DeleteProductHandler(fakeUOW);
            var products = await fakeUOW.Product.GetAllAsync();
            var productName = products.First().Name;

            await handler.Handle(new DeleteProductCommand(products.First().Name), CancellationToken.None);

            var result = await fakeUOW.Product.GetAsync(productName);

            result.ShouldBeNull();
        }

        [Theory]
        [InlineData("New Element")]
        public async Task UpdateProductHandlerTest(string newName)
        {
            var fakeUOW = MockPharmacyUoW.GetUnitOfWorks().Object;
            var products = await fakeUOW.Product.GetAllAsync();
            var productForUpdate = products.First();
            productForUpdate.Name = newName;
            var handler = new UpdateProductHandler(fakeUOW, _mapper);
            var productModel = _mapper.Map<ProductModel>(productForUpdate);

            await handler.Handle(new UpdateProductCommand(productForUpdate.Id, productModel), CancellationToken.None);
            var result = await fakeUOW.Product.GetAsync(productForUpdate.Id);

            result.Name.ShouldBe(newName);
        }
    }
}
