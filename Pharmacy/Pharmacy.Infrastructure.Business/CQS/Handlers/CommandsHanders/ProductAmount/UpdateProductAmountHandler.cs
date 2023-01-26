using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductAmount
{
    public class UpdateProductAmountHandler : IRequestHandler<UpdateProductAmountCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProductAmountCommand request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId);

            _mapper.Map(request.Model, productAmount);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}