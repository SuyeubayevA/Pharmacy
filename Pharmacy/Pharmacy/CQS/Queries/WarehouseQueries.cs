using MediatR;
using Pharmacy.Domain.Core;

namespace Pharmacy.Queries
{
    public class GetWarehouseByIdQuery : IRequest<Warehouse>
    {
        public int _id { get; }

        public GetWarehouseByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllWarehousesQuery : IRequest<Warehouse[]>
    {

    }
}
