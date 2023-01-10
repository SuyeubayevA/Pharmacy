using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetProductTypeByIdQuery : IRequest<CQRSResponse<ProductTypeDetailsDTO>>
    {
        public int _id { get; }

        public GetProductTypeByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductTypesQuery : IRequest<CQRSResponse<List<ProductTypeDTO>>>
    {

    }
}
