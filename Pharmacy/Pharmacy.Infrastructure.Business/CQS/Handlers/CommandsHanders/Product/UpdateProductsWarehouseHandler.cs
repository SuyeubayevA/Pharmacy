using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;
using MediatR;
using Pharmacy.Infrastructure.Data.Abstracts;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Product
{
    public class UpdateProductsWarehouseHandler : IRequestHandler<UpdateProductsWarehouseCommand, Unit>
    {
        private readonly IUnitOfWork _uow;

        public UpdateProductsWarehouseHandler(IUnitOfWork uow)
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