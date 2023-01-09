
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDetailDTO>
    {
        private readonly UnitOfWork _uow;

        public GetProductByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductDetailDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Product.GetAsync(request._id);
        }
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ProductDTO[]>
    {
        private readonly UnitOfWork _uow;
        public GetAllProductsHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<ProductDTO[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Product.GetAllASync();
        }
    }
}