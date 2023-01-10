using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public record GetWarehouseByIdQuery(int Id) : IRequest<CQRSResponse<WarehouseDetailsDTO>>
    {
    }

    public record GetAllWarehousesQuery : IRequest<CQRSResponse<List<WarehouseDTO>>>
    {

    }
}
