using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data.DTO;

namespace Pharmacy.Queries
{
    public class GetProductTypeByIdQuery : IRequest<ProductTypeDetailsDTO>
    {
        public int _id { get; }

        public GetProductTypeByIdQuery(int id)
        {
            this._id = id;
        }
    }

    public class GetAllProductTypesQuery : IRequest<ProductTypeDTO[]>
    {

    }
}
