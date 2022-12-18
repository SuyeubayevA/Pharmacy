
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly UnitOfWork _uow;

        public GetProductByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Product.GetAsync(request._id);
        }
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Product[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllProductsHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Product[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Product.GetAllASync();
        }
    }
}