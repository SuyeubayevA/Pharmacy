
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetWarehouseByIdHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseDetailsDTO>
    {
        private readonly UnitOfWork _uow;

        public GetWarehouseByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<WarehouseDetailsDTO> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Warehouse.GetAsync(request._id);
        }
    }

    public class GetAllWarehouseHandler : IRequestHandler<GetAllWarehousesQuery, WarehouseDTO[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<WarehouseDTO[]> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _uow.Warehouse.GetAllASync();
        }
    }
}