using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductType
{

    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteProductTypeHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            _uow.ProductType.Delete(request.Id);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}