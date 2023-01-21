
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
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

    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _uow.Product.GetAllAsync();

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productsDTO.OrderByDescending(p => p.Name);
        }
    }
}