using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.ProductType.GetAsync(request.Model.Name) != null)
            {
                Results.BadRequest("The object lred exist !");
            }

            if (request.Model is ProductTypeModel)
            {
                var productType = _mapper.Map<ProductType>(request.Model);
                _uow.ProductType.Create(productType);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(productType);
                }
            }

            return Results.BadRequest(request.Model);
        }
    }

    public class UpdateProductTypeHandler : IRequestHandler<UpdateProductTypeCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Model.Id);

            if (productType == null) { return Results.NotFound(); }

            _mapper.Map(request.Model, productType);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(productType);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Id);

            if (productType == null) { return Results.NotFound(); }

            _uow.ProductType.Delete(productType.Id);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(productType);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }
}