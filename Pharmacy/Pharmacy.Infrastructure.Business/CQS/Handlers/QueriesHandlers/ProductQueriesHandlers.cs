
using AutoMapper;
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, CQRSResponse<ProductDetailDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CQRSResponse<ProductDetailDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.Id);
            var response = new CQRSResponse<ProductDetailDTO>();

            if (product == null)
            {
                response.IsSuccess = false;
                response.Message = "Product did not find.";
            }
            else
            {
                var productDetailDTO = _mapper.Map<ProductDetailDTO>(product);

                response.IsSuccess = true;
                response.Model = productDetailDTO;
            }
            return response;
        }
    }

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, CQRSResponse<List<ProductDTO>>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _uow.Product.GetAllASync();
            var response = new CQRSResponse<List<ProductDTO>>();

            if(products == null)
            {
                response.IsSuccess = false;
                response.Message = "Products did not find.";
            }
            else
            {
                var productsDTO = _mapper.Map<List<ProductDTO>>(products);
                productsDTO.OrderByDescending(p => p.Name);

                response.IsSuccess = true;
                response.Model = productsDTO;
            }

            return response;
        }
    }
}