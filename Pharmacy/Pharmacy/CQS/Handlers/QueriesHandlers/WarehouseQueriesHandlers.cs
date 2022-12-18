
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetWarehouseByIdHandler : IRequestHandler<GetWarehouseByIdQuery, Warehouse>
    {
        private readonly UnitOfWork _uow;

        public GetWarehouseByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Warehouse> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Warehouse.GetAsync(request._id);
        }
    }

    public class GetAllWarehouseHandler : IRequestHandler<GetAllWarehousesQuery, Warehouse[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Warehouse[]> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Warehouse.GetAllASync();
        }
    }
}