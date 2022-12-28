using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Queries
{
    public class GetProductAmountByIdQuery : IRequest<ProductAmountDetailsDTO>
    {
        public int _id { get; }

        public GetProductAmountByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductAmountsQuery : IRequest<ProductAmountDTO[]>
    {

    }
}
