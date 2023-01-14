using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, CQRSResponse<Warehouse>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<Warehouse>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<Warehouse>();

            if (await _uow.Warehouse.GetAsync(request.Model.Name) != null)
            {
                result.Message = "The object already exist !";

                return result;
            }

            var warehouse = _mapper.Map<Warehouse>(request.Model);
            var isCreated = await _uow.Warehouse.Create(warehouse);

            if (isCreated)
            {
                result.IsSuccess = true;
                result.Model = warehouse;

                return result;
            }

            result.Model = warehouse;
            return result;
        }
    }

    public class UpdateWarehouseHandler : IRequestHandler<UpdateWarehouseCommand, CQRSResponse<Warehouse>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<Warehouse>> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.Model.Name);
            var result = new CQRSResponse<Warehouse>();

            if (warehouse == null)
            {
                result.Message = "There is no Warehouse with such Id";
                return result;
            }

            _mapper.Map(request.Model, warehouse);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = warehouse;

                return result;
            }
            else
            {
                result.Model = warehouse;
                return result;
            }
        }
    }

    public class DeleteWarehouseHandler : IRequestHandler<DeleteWarehouseCommand, CQRSResponse<Warehouse>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<Warehouse>> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.WarehouseName);
            var result = new CQRSResponse<Warehouse>();

            if (warehouse == null)
            {
                result.Message = "Didn't find this Warehouse";
                return result;
            }

            _uow.Warehouse.Delete(warehouse.Id);

            if (await _uow.SaveAsync())
            {
                result.Model = warehouse;
                result.IsSuccess = true;

                return result;
            }
            else
            {
                return result;
            }
        }
    }
}