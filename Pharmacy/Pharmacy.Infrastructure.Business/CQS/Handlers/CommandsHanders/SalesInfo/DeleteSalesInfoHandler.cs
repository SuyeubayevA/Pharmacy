using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.SalesInfo
{

    public class DeleteSalesInfoHandler : IRequestHandler<DeleteSalesInfoCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteSalesInfoHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request.ProductId, 0);

            _uow.SalesInfo.Delete(salesInfo.Id);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}