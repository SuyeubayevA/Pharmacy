using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetWarehouseByIdQuery : IRequest<WarehouseDetailsDTO>
    {
        public int _id { get; }

        public GetWarehouseByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllWarehousesQuery : IRequest<WarehouseDTO[]>
    {

    }
}
