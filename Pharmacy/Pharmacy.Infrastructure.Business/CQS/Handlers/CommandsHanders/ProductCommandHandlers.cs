using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using MediatR;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.Product.GetAsync(request.Model.Name) != null)
            {
                Results.BadRequest("The object already exist !");
            }

            if (request.Model is not null)
            {
                var product = _mapper.Map<Product>(request.Model);
                _uow.Product.Create(product);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(product);
                }
            }

            return Results.BadRequest(request.Model);
        }
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.Id);

            if (product == null) { return Results.NotFound(); }

            _mapper.Map(request.Model, product);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(product);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class UpdateProductsWarehouseHandler : IRequestHandler<UpdateProductsWarehouseCommand, IResult>
    {
        private readonly UnitOfWork _uow;

        public UpdateProductsWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IResult> Handle(UpdateProductsWarehouseCommand request, CancellationToken cancellationToken)
        {
            var result = _uow.Product.UpdateWarehouseLink(request.Id, request.WarehouseId, request.Amount, request.Discount);

            if (result && await _uow.SaveAsync())
            {
                return Results.Ok();
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, IResult>
    {
        private readonly UnitOfWork _uow;

        public DeleteProductHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.ProductName);

            if (product == null) { return Results.NotFound(); }

            _uow.Product.Delete(product.Id);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(product);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }
}