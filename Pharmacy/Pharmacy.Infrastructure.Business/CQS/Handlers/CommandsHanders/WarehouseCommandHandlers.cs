using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.Warehouse.GetAsync(request.Model.Name) != null)
            {
                throw new Exception("The object already exist !");
            }

            var warehouse = _mapper.Map<Warehouse>(request.Model);
            _uow.Warehouse.Create(warehouse);

            await _uow.SaveAsync();


            return Unit.Value;
        }
    }

    public class UpdateWarehouseHandler : IRequestHandler<UpdateWarehouseCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.Model.Name);

            _uow.Warehouse.Update(warehouse);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

    public class DeleteWarehouseHandler : IRequestHandler<DeleteWarehouseCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.WarehouseName);

            _uow.Warehouse.Delete(warehouse.Id);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}