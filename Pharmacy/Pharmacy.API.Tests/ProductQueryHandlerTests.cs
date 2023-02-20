﻿
using AutoMapper;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductQueryHandlerTests()
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
        public async Task GetAllProductsHandlerTest()
        {
            var handler = new GetAllProductsHandler(_uow, _mapper);
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            result.Count().ShouldBeGreaterThan(0);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetProductByIdHandlerTest(int id)
        {
            var handler = new GetProductByIdHandler(_uow, _mapper);
            var result = await handler.Handle(new GetProductByIdQuery(id), CancellationToken.None);

            result.ProductTypeId.ShouldBe(1);
        }
    }
}