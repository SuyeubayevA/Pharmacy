
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetProductTypeByIdHandler : IRequestHandler<GetProductTypeByIdQuery, ProductTypeDetailsDTO>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetProductTypeByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ProductTypeDetailsDTO> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Id);

            return _mapper.Map<ProductTypeDetailsDTO>(productType);
        }
    }

    public class GetAllProductTypessHandler : IRequestHandler<GetAllProductTypesQuery, IEnumerable<ProductTypeDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAllProductTypessHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }
        public async Task<IEnumerable<ProductTypeDTO>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes = await _uow.ProductType.GetAllAsync();
            var productsTypesDTO = _mapper.Map<List<ProductTypeDTO>>(productTypes);

            return productsTypesDTO.OrderByDescending(p => p.Name);
        }
    }
}