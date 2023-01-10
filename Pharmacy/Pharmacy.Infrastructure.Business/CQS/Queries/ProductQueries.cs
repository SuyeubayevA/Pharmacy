using MediatR;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<CQRSResponse<ProductDetailDTO>> { }

    public record GetAllProductsQuery : IRequest<CQRSResponse<List<ProductDTO>>> { }
}
