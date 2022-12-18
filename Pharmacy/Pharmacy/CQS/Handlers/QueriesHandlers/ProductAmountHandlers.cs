
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductAmountByIdHandler : IRequestHandler<GetProductAmountByIdQuery, ProductAmount>
    {
        private readonly UnitOfWork _uow;

        public GetProductAmountByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductAmount> Handle(GetProductAmountByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductAmount.GetAsync(request._id);
        }
    }

    public class GetAllProductAmountsHandler : IRequestHandler<GetAllProductAmountsQuery, ProductAmount[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllProductAmountsHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductAmount[]> Handle(GetAllProductAmountsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductAmount.GetAllASync();
        }
    }
}