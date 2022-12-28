
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductTypeByIdHandler : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDetailsDTO>
    {
        private readonly UnitOfWork _uow;

        public GetProductTypeByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductTypeDetailsDTO> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductType.GetAsync(request._id);
        }
    }

    public class GetAllProductTypessHandler : IRequestHandler<GetAllProductTypesQuery, ProductTypeDTO[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllProductTypessHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductTypeDTO[]> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.ProductType.GetAllASync();
        }
    }
}