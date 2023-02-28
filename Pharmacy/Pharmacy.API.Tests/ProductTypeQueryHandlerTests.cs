
using AutoMapper;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.ProductType;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductTypeQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductTypeQueryHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PharmacyMappingProfile());
                cfg.AddProfile(new PharmacyModelsToDTOMappingProfile());
            });

            _mapper = mapperConfig.CreateMapper();
            _uow = MockPharmacyUoW.GetUnitOfWorks().Object;
        }

        //[Fact]
        //public async Task GetAllProductTypesHandlerTest()
        //{
        //    var handler = new GetAllProductTypesHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetAllProductTypesQuery(), CancellationToken.None);

        //    result.Count().ShouldBeGreaterThan(0);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async Task GetProductTypeByIdHandlerTest(int id)
        //{
        //    var handler = new GetProductTypeByIdHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetProductTypeByIdQuery(id), CancellationToken.None);

        //    result.Id.ShouldBe(1);
        //}
    }
}
