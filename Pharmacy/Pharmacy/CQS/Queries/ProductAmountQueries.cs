using MediatR;
using Pharmacy.Domain.Core;

namespace Pharmacy.Queries
{
    public class GetProductAmountByIdQuery : IRequest<ProductAmount>
    {
        public int _id { get; }

        public GetProductAmountByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductAmountsQuery : IRequest<ProductAmount[]>
    {

    }
}
