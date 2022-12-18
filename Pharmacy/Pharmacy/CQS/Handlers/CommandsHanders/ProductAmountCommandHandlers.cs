using AutoMapper;
using MediatR;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Handlers.CommandsHanders
{
    public class CreateProductAmountHandler : IRequestHandler<CreateProductAmountCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateProductAmountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _uow.ProductAmount.GetAsync(request._model.WarehouseId, request._model.ProductId) != null)
                {
                    Results.BadRequest("The object lred exist !");
                }

                if (request._model is ProductAmountModel)
                {
                    var productAmount = _mapper.Map<ProductAmount>(request._model);
                    _uow.ProductAmount.Create(productAmount);

                    if (await _uow.SaveAsync())
                    {
                        return Results.Ok(productAmount);
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

    public class UpdateProductAmountHandler : IRequestHandler<UpdateProductAmountCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateProductAmountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productAmount = await _uow.ProductAmount.GetAsync(request._model.WarehouseId, request._model.ProductId);

                if (productAmount == null) { return Results.NotFound(); }

                _mapper.Map(request._model, productAmount);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(productAmount);
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

    public class DeleteProductAmountHandler : IRequestHandler<DeleteProductAmountCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteProductAmountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productAmount = await _uow.ProductAmount.GetAsync(request._Id);

                if (productAmount == null) { return Results.NotFound(); }

                _uow.ProductAmount.Delete(productAmount.Id);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(productAmount);
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