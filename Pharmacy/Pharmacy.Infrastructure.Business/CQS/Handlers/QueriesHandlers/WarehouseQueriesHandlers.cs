
using AutoMapper;
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Infrastructure.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetWarehouseByIdHandler : IRequestHandler<GetWarehouseByIdQuery, CQRSResponse<WarehouseDetailsDTO>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetWarehouseByIdHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<WarehouseDetailsDTO>> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request._id);
            var response = new CQRSResponse<WarehouseDetailsDTO>();

            if (warehouse == null)
            {
                response.IsSuccess = false;
                response.Message = "Warehouse did not find.";
            }
            else
            {
                var warehouseDetailDTO = _mapper.Map<WarehouseDetailsDTO>(warehouse);

                response.IsSuccess = true;
                response.Model = warehouseDetailDTO;
            }

            return response;
        }
    }

    public class GetAllWarehouseHandler : IRequestHandler<GetAllWarehousesQuery, CQRSResponse<List<WarehouseDTO>>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<List<WarehouseDTO>>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _uow.Warehouse.GetAllASync();
            var response = new CQRSResponse<List<WarehouseDTO>>();

            if (warehouses == null)
            {
                response.IsSuccess = false;
                response.Message = "Warehouses did not find.";
            }
            else
            {
                var productsTypesDTO = _mapper.Map<List<WarehouseDTO>>(warehouses);
                productsTypesDTO.OrderByDescending(p => p.Name);

                response.IsSuccess = true;
                response.Model = productsTypesDTO;
            }

            return response;
        }
    }
}