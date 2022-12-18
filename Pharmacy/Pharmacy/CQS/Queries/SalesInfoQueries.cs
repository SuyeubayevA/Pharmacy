using MediatR;
using Pharmacy.Domain.Core;

namespace Pharmacy.Queries
{
    public class GetSalesInfoByIdQuery : IRequest<SalesInfo>
    {
        public int _id { get; }

        public GetSalesInfoByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllSalesInfosQuery : IRequest<SalesInfo[]>
    {

    }
}
