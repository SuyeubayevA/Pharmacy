
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductAmountByIdHandler : IRequestHandler<GetProductAmountByIdQuery, ProductAmountDetailsDTO>
    {
        private readonly UnitOfWork _uow;

        public GetProductAmountByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductAmountDetailsDTO> Handle(GetProductAmountByIdQuery request, CancellationToken cancellationToken)
        {
            var test = await _uow.ProductAmount.GetAsync(request._id);
            return test;
        }
    }

    public class GetAllProductAmountsHandler : IRequestHandler<GetAllProductAmountsQuery, ProductAmountDTO[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllProductAmountsHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductAmountDTO[]> Handle(GetAllProductAmountsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductAmount.GetAllASync();
        }
    }
}