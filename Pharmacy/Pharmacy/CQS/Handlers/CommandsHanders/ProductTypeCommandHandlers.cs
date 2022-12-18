using AutoMapper;
using MediatR;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Handlers.CommandsHanders
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
            try
            {
                if (await _uow.ProductType.GetAsync(request._model.Name) != null)
                {
                    Results.BadRequest("The object lred exist !");
                }

                if (request._model is ProductTypeModel)
                {
                    var productType = _mapper.Map<ProductType>(request._model);
                    _uow.ProductType.Create(productType);

                    if (await _uow.SaveAsync())
                    {
                        return Results.Ok(productType);
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
            try
            {
                var productType = await _uow.ProductType.GetAsync(request._model.Id);

                if (productType == null) { return Results.NotFound(); }

                _mapper.Map(request._model, productType);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(productType);
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
            try
            {
                var productType = await _uow.ProductType.GetAsync(request._Id);

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
            catch
            {
                return Results.StatusCode(500);
            }
        }
    }
}