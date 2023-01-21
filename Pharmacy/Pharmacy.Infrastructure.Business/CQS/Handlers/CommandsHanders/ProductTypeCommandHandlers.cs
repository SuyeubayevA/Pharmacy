using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.ProductType.GetAsync(request.Model.Name) != null)
            {
                throw new Exception("The object already exist !");
            }

            var productType = _mapper.Map<ProductType>(request.Model);
            _uow.ProductType.Create(productType);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

    public class UpdateProductTypeHandler : IRequestHandler<UpdateProductTypeCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Model.Id);
            _mapper.Map(request.Model, productType);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

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