
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetProductTypeByIdHandler : IRequestHandler<GetProductTypeByIdQuery, CQRSResponse<ProductTypeDetailsDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductTypeByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductTypeDetailsDTO>> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Id);
            var response = new CQRSResponse<ProductTypeDetailsDTO>();

            if (productType == null)
            {
                response.IsSuccess = false;
                response.Message = "ProductType did not find.";
            }
            else
            {
                var productDetailDTO = _mapper.Map<ProductTypeDetailsDTO>(productType);

                response.IsSuccess = true;
                response.Model = productDetailDTO;
            }

            return response;
        }
    }

    public class GetAllProductTypessHandler : IRequestHandler<GetAllProductTypesQuery, CQRSResponse<List<ProductTypeDTO>>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductTypessHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }
        public async Task<CQRSResponse<List<ProductTypeDTO>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _uow.ProductType.GetAllASync();
            var response = new CQRSResponse<List<ProductTypeDTO>>();

            if (productTypes == null)
            {
                response.IsSuccess = false;
                response.Message = "ProductTypes did not find.";
            }
            else
            {
                var productsTypesDTO = _mapper.Map<List<ProductTypeDTO>>(productTypes);
                productsTypesDTO.OrderByDescending(p => p.Name);

                response.IsSuccess = true;
                response.Model = productsTypesDTO;
            }

            return response;
        }
    }
}