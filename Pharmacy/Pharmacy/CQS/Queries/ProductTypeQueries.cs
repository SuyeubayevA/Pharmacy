using MediatR;
using Pharmacy.Domain.Core;

namespace Pharmacy.Queries
{
    public class GetProductTypeByIdQuery : IRequest<ProductType>
    {
        public int _id { get; }

        public GetProductTypeByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductTypesQuery : IRequest<ProductType[]>
    {

    }
}
