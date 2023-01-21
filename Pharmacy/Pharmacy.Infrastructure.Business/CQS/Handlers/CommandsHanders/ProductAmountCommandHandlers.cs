using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductAmountHandler : IRequestHandler<CreateProductAmountCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductAmountCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<ProductAmount>();

            if (await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId) != null)
            {
                throw new Exception("The object already exist !");
            }

            var productAmount = _mapper.Map<ProductAmount>(request.Model);
            _uow.ProductAmount.Create(productAmount);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

    public class UpdateProductAmountHandler : IRequestHandler< UpdateProductAmountCommand, Unit>
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