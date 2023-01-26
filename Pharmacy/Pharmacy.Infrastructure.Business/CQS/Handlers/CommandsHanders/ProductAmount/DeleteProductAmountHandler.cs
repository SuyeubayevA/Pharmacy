using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductAmount
{

    public class DeleteProductAmountHandler : IRequestHandler<DeleteProductAmountCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteProductAmountHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteProductAmountCommand request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request.Id);

            _uow.ProductAmount.Delete(productAmount.Id);

            await _uow.SaveAsync();
            return Unit.Value;
        }
    }
}