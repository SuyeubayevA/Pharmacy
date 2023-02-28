
using AutoMapper;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Warehouse;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class WarehouseQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public WarehouseQueryHandlerTests()
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
        //public async Task GetAllWarehouseHandlerTest()
        //{
        //    var handler = new GetAllWarehouseHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetAllWarehousesQuery(), CancellationToken.None);

        //    result.Count().ShouldBeGreaterThan(0);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async Task GetWarehouseByIdHandlerTest(int id)
        //{
        //    var handler = new GetWarehouseByIdHandler(_uow, _mapper);
        //    var result = await handler.Handle(new GetWarehouseByIdQuery(id), CancellationToken.None);

        //    result.Id.ShouldBe(1);
        //}
    }
}
