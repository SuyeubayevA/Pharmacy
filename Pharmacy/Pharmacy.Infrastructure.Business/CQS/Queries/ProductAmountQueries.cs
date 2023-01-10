using MediatR;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public record GetProductAmountByIdQuery(int Id) : IRequest<CQRSResponse<ProductAmountDetailsDTO>> { }

    public record GetAllProductAmountsQuery : IRequest<CQRSResponse<List<ProductAmountDTO>>> { }
}
