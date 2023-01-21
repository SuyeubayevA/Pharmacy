using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using MediatR;
using AutoMapper;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.Product.GetAsync(request.Model.Name) != null)
            {
                throw new Exception("The object already exist !");
            }

            var product = _mapper.Map<Product>(request.Model);
            _uow.Product.Create(product);
            await _uow.SaveAsync();

            return Unit.Value;

        }
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.Id);
            _mapper.Map(request.Model, product);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

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