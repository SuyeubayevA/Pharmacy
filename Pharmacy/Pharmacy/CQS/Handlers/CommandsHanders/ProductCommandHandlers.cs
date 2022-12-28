using AutoMapper;
using MediatR;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Handlers.CommandsHanders
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
            try
            {
                if (await _uow.Product.GetAsync(request._model.Name) != null)
                {
                    Results.BadRequest("The object already exist !");
                }

                if (request._model is ProductModel)
                {
                    var product = _mapper.Map<Product>(request._model);
                    _uow.Product.Create(product);

                    if (await _uow.SaveAsync())
                    {
                        return Results.Ok(product);
                    }
                }
            }
            catch
            {
                return Results.StatusCode(500);
            }

            return Results.BadRequest(request._model);
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
            try
            {
                var product = await _uow.Product.GetAsync(request._Id);

                if (product == null) { return Results.NotFound(); }

                _mapper.Map(request._model, product);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(product);
                }
                else
                {
                    return Results.StatusCode(500);
                }
            }
            catch
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class UpdateProductsWarehouseHandler : IRequestHandler<UpdateProductsWarehouseCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductsWarehouseHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateProductsWarehouseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var product = await _uow.Product.GetAsync(request._Id);

                //if (product == null) { return Results.NotFound(); }

                var result = _uow.Product.UpdateWarehouseLink(request._Id, request._WarehouseId, request._Amount, request._Discount);

                if (result && await _uow.SaveAsync())
                {
                    return Results.Ok();
                }
                else
                {
                    return Results.StatusCode(500);
                }
            }
            catch(Exception ex)
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _uow.Product.GetAsync(request._productName);

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
            catch
            {
                return Results.StatusCode(500);
            }
        }
    }
}