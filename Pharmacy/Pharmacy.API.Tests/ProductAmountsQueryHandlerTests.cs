
using AutoMapper;
using Pharmacy.API.Tests.Mocks;
using Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product;
using Pharmacy.Infrastructure.Data.Abstracts;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Profiles;
using Shouldly;

namespace Pharmacy.API.Tests
{
    public class ProductAmountsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductAmountsQueryHandlerTests()
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
        //public async Task GetAllProductAmountsHandlerTest()
        //{
        //    var handler = new GetAllProductAmountsHandler(_uow, _mapper);
        //    var tt = await _uow.ProductAmount.GetAllAsync();
        //    var result = await handler.Handle(new GetAllProductAmountsQuery(), CancellationToken.None);

        //    result.Count().ShouldBeGreaterThan(0);
        //}

        //[Theory]
        //[InlineData(1)]
        //public async Task GetProductAmountByIdHandlerTest(int id)
        //{
        //    var handler = new GetProductAmountByIdHandler(_uow, _mapper);
        //    var tt = await _uow.ProductAmount.GetAllAsync();
        //    var result = await handler.Handle(new GetProductAmountByIdQuery(id), CancellationToken.None);

        //     result.Id.ShouldBe(1);
        //}
    }
}
