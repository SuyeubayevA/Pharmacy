
using AutoMapper;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.SalesInfo;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class SalesInfosQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public SalesInfosQueryHandlerTests()
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
        //public async Task GetAllSalesInfoHandlerTest()
        //{
        //    var handler = new GetAllSalesInfoHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetAllSalesInfosQuery(), CancellationToken.None);

        //    result.Count().ShouldBeGreaterThan(0);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async Task GetSalesInfoByIdHandlerTest(int id)
        //{
        //    var handler = new GetSalesInfoByIdHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetSalesInfoByIdQuery(id), CancellationToken.None);

        //    result.Id.ShouldBe(1);
        //}
    }
}
