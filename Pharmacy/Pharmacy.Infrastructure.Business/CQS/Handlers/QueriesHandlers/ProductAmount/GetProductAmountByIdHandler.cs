
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.QueriesHandlers.Product
{
    public class GetProductAmountByIdHandler : IRequestHandler<GetProductAmountByIdQuery, ProductAmountDetailsDTO>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductAmountByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ProductAmountDetailsDTO> Handle(GetProductAmountByIdQuery request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request.Id);

            return _mapper.Map<ProductAmountDetailsDTO>(productAmount);
        }
    }
}