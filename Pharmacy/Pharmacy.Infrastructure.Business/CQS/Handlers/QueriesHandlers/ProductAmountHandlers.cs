
using AutoMapper;
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetProductAmountByIdHandler : IRequestHandler<GetProductAmountByIdQuery, CQRSResponse<ProductAmountDetailsDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductAmountByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductAmountDetailsDTO>> Handle(GetProductAmountByIdQuery request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request._id);
            var response = new CQRSResponse<ProductAmountDetailsDTO>();

            if (productAmount == null)
            {
                response.IsSuccess = false;
                response.Message = "ProductAmount did not find.";
            }
            else
            {
                var productAmountDetailsDTO = ObjectMapper.Mapper.Map<ProductAmountDetailsDTO>(productAmount);

                response.IsSuccess = true;
                response.Model = productAmountDetailsDTO;
            }

            return response;
        }
    }

    public class GetAllProductAmountsHandler : IRequestHandler<GetAllProductAmountsQuery, CQRSResponse<List<ProductAmountDTO>>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductAmountsHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<List<ProductAmountDTO>>> Handle(GetAllProductAmountsQuery request, CancellationToken cancellationToken)
        {
            var productAmounts = await _uow.ProductAmount.GetAllASync();
            var response = new CQRSResponse<List<ProductAmountDTO>>();

            if (productAmounts == null)
            {
                response.IsSuccess = false;
                response.Message = "ProductAmounts did not find.";
            }
            else
            {
                var productAmountsDTO = _mapper.Map<List<ProductAmountDTO>>(productAmounts);
                productAmountsDTO.OrderByDescending(p => p.ProductName);

                response.IsSuccess = true;
                response.Model = productAmountsDTO;
            }

            return response;
        }
    }
}