using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetWarehouseByIdQuery : IRequest<CQRSResponse<WarehouseDetailsDTO>>
    {
        public int _id { get; }

        public GetWarehouseByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllWarehousesQuery : IRequest<CQRSResponse<List<WarehouseDTO>>>
    {

    }
}
