using AutoMapper;
using MediatR;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Handlers.CommandsHanders
{
    public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.Warehouse.GetAsync(request.Model.Name) != null)
            {
                Results.BadRequest("The object lred exist !");
            }

            if (request.Model is WarehouseModel)
            {
                var warehouse = _mapper.Map<Warehouse>(request.Model);
                _uow.Warehouse.Create(warehouse);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(warehouse);
                }
            }

            return Results.BadRequest(request.Model);
        }
    }

    public class UpdateWarehouseHandler : IRequestHandler<UpdateWarehouseCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.Model.Name);

            if (warehouse == null) { return Results.NotFound(); }

            _mapper.Map(request.Model, warehouse);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(warehouse);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class DeleteWarehouseHandler : IRequestHandler<DeleteWarehouseCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _uow.Warehouse.GetAsync(request.WarehouseName);

            if (warehouse == null) { return Results.NotFound(); }

            _uow.Warehouse.Delete(warehouse.Id);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(warehouse);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }
}