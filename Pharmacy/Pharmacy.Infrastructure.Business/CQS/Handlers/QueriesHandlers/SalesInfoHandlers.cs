
using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetSalesInfoByIdHandler : IRequestHandler<GetSalesInfoByIdQuery, CQRSResponse<SalesInfoDetailsDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetSalesInfoByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<SalesInfoDetailsDTO>> Handle(GetSalesInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var sailsInfo = await _uow.SalesInfo.GetAsync(request.Id);
            var response = new CQRSResponse<SalesInfoDetailsDTO>();

            if (sailsInfo == null)
            {
                response.IsSuccess = false;
                response.Message = "SalesInfo did not find.";
            }
            else
            {
                var salesInfoDetailDTO = _mapper.Map<SalesInfoDetailsDTO>(sailsInfo);

                response.IsSuccess = true;
                response.Model = salesInfoDetailDTO;
            }

            return response;
        }
    }

    public class GetAllSalesInfoHandler : IRequestHandler<GetAllSalesInfosQuery, CQRSResponse<List<SalesInfoDTO>>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<List<SalesInfoDTO>>> Handle(GetAllSalesInfosQuery request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAllASync();
            var response = new CQRSResponse<List<SalesInfoDTO>>();

            if (salesInfo == null)
            {
                response.IsSuccess = false;
                response.Message = "SalesInfo did not find.";
            }
            else
            {
                var salesInfoDTO = _mapper.Map<List<SalesInfoDTO>>(salesInfo);
                salesInfoDTO.OrderByDescending(p => p.CreatedDate);

                response.IsSuccess = true;
                response.Model = salesInfoDTO;
            }

            return response;
        }
    }
}