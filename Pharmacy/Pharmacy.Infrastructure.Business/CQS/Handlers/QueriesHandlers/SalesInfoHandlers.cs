
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetSalesInfoByIdHandler : IRequestHandler<GetSalesInfoByIdQuery, SalesInfoDetailsDTO>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetSalesInfoByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<SalesInfoDetailsDTO> Handle(GetSalesInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var sailsInfo = await _uow.SalesInfo.GetAsync(request.Id);

            return _mapper.Map<SalesInfoDetailsDTO>(sailsInfo);
        }
    }

    public class GetAllSalesInfoHandler : IRequestHandler<GetAllSalesInfosQuery, IEnumerable<SalesInfoDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SalesInfoDTO>> Handle(GetAllSalesInfosQuery request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAllAsync();
            var salesInfoDTO = _mapper.Map<IEnumerable<SalesInfoDTO>>(salesInfo);

            return salesInfoDTO.OrderByDescending(p => p.CreatedDate);
        }
    }
}