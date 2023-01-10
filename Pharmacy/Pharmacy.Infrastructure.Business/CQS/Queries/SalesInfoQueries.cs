using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetSalesInfoByIdQuery : IRequest<CQRSResponse<SalesInfoDetailsDTO>>
    {
        public int _id { get; }

        public GetSalesInfoByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllSalesInfosQuery : IRequest<CQRSResponse<List<SalesInfoDTO>>>
    {

    }
}
