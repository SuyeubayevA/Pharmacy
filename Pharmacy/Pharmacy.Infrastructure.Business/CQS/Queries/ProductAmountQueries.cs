using MediatR;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetProductAmountByIdQuery : IRequest<CQRSResponse<ProductAmountDetailsDTO>>
    {
        public int _id { get; }

        public GetProductAmountByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductAmountsQuery : IRequest<CQRSResponse<List<ProductAmountDTO>>>
    {

    }
}
