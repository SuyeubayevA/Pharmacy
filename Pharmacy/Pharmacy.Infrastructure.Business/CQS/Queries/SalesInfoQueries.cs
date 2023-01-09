using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Infrastructure.Queries
{
    public class GetSalesInfoByIdQuery : IRequest<SalesInfoDetailsDTO>
    {
        public int _id { get; }

        public GetSalesInfoByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllSalesInfosQuery : IRequest<SalesInfoDTO[]>
    {

    }
}
