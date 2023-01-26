using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;
using MediatR;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Product
{
    public class UpdateProductsWarehouseHandler : IRequestHandler<UpdateProductsWarehouseCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public UpdateProductsWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(UpdateProductsWarehouseCommand request, CancellationToken cancellationToken)
        {
            _uow.Product.UpdateWarehouseLink(request.Id, request.WarehouseId, request.Amount, request.Discount);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}