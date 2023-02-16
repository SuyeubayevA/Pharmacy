
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDetailDTO>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ProductDetailDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.Id);

            return _mapper.Map<ProductDetailDTO>(product);
        }
    }
}