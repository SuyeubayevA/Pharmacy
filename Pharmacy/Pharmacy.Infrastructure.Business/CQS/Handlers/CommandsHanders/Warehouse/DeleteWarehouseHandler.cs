using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Warehouse
{

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