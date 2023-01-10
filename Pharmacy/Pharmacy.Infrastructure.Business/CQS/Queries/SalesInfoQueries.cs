using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public record GetSalesInfoByIdQuery(int Id) : IRequest<CQRSResponse<SalesInfoDetailsDTO>> { }

    public record GetAllSalesInfosQuery : IRequest<CQRSResponse<List<SalesInfoDTO>>>
    {

    }
}
