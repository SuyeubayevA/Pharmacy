using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;
using MediatR;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.Product
{

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteProductHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.ProductName);

            _uow.Product.Delete(product.Id);
            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}