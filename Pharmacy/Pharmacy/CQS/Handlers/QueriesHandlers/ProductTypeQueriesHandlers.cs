
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductTypeByIdHandler : IRequestHandler<GetProductTypeByIdQuery, ProductType>
    {
        private readonly UnitOfWork _uow;

        public GetProductTypeByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductType> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductType.GetAsync(request._id);
        }
    }

    public class GetAllProductTypessHandler : IRequestHandler<GetAllProductTypesQuery, ProductType[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllProductTypessHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductType[]> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductType.GetAllASync();
        }
    }
}