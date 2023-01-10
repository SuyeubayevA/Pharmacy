using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public record GetProductTypeByIdQuery(int Id) : IRequest<CQRSResponse<ProductTypeDetailsDTO>> { }

    public record GetAllProductTypesQuery : IRequest<CQRSResponse<List<ProductTypeDTO>>>
    {

    }
}
