using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Abstracts;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductAmount
{
    public class CreateProductAmountCommandHandler : IRequestHandler<CreateProductAmountCommand, Unit>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductAmountCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductAmountCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId) != null)
            {
                throw new Exception("The object already exist !");
            }

            var productAmount = _mapper.Map<Domain.Core.ProductAmount>(request.Model);
            _uow.ProductAmount.Create(productAmount);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}