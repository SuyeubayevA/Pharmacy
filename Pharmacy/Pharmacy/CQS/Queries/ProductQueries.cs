using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int _id { get; }

        public GetProductByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductsQuery : IRequest<Product[]>
    {

    }
}
