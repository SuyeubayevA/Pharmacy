
using AutoMapper;
using Azure.Core;
using Pharmacy.API.Tests.Helpers;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Product;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Models;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductCommandsHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductCommandsHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductMappingProfile());
                cfg.AddProfile(new ProductToDTOMappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
            _uow = MockPharmacyUoW.GetUnitOfWorks().Object;
        }

        [Fact]
        public async Task CreateProductHandlerTest()
        {
            var handler = new CreateProductHandler(_uow, _mapper);
            var newProduct = Factory.CreateItem(EntityType.Product);
            var newProductModel = _mapper.Map<ProductModel>(newProduct);

            await handler.Handle(new CreateProductCommand(newProductModel), CancellationToken.None);

            await _uow.Product.GetAsync(newProductModel.Id).ShouldNotBeNull();
        }

        [Fact]
        public async Task DeleteProductHandlerTest()
        {
            var handler = new DeleteProductHandler(_uow);
            var products = await _uow.Product.GetAllAsync();
            if(products != null && products.Count() > 0)
            {
                var productName = products.First().Name;
                await handler.Handle(new DeleteProductCommand(productName), CancellationToken.None);

                var result = await _uow.Product.GetAsync(productName);

                result.ShouldBeNull();
            }
        }

        [Theory]
        [InlineData("New Element")]
        public async Task UpdateProductHandlerTest(string newName)
        {
            var handler = new UpdateProductHandler(_uow, _mapper);
            var products = await _uow.Product.GetAllAsync();
            if (products != null && products.Count() > 0)
            {
                var productForUpdate = products.First();
                productForUpdate.Name = newName;
                var productModel = _mapper.Map<ProductModel>(productForUpdate);

                await handler.Handle(new UpdateProductCommand(productForUpdate.Id, productModel), CancellationToken.None);

                var result = await _uow.Product.GetAsync(productForUpdate.Id);

                result.Name.ShouldBe(newName);
            }
        }
    }
}
