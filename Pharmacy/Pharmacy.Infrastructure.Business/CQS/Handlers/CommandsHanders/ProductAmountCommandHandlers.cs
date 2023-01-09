using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
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
            if (await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId) != null)
            {
                Results.BadRequest("The object already exist !");
            }

            if (request.Model is ProductAmountModel)
            {
                var productAmount = _mapper.Map<ProductAmount>(request.Model);
                _uow.ProductAmount.Create(productAmount);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(productAmount);
                }
            }

            return Results.BadRequest(request.Model);
        }
    }

    public class UpdateProductAmountHandler : IRequestHandler< UpdateProductAmountCommand, IResult>
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
            var productAmount = await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId);

            if (productAmount == null) { return Results.NotFound(); }

            _mapper.Map(request.Model, productAmount);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(productAmount);
            }
            else
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
            var productAmount = await _uow.ProductAmount.GetAsync(request.Id);

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
    }
}