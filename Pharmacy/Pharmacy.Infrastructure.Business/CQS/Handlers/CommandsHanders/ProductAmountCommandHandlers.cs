using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductAmountHandler : IRequestHandler<CreateProductAmountCommand, CQRSResponse<ProductAmount>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductAmount>> Handle(CreateProductAmountCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<ProductAmount>();

            if (await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId) != null)
            {
                result.Message = "The object already exist !";

                return result;
            }

            var productAmount = _mapper.Map<ProductAmount>(request.Model);
            _uow.ProductAmount.Create(productAmount);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = productAmount;

                return result;
            }

            result.Model = productAmount;
            return result;
        }
    }

    public class UpdateProductAmountHandler : IRequestHandler< UpdateProductAmountCommand, CQRSResponse<ProductAmount>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductAmount>> Handle(UpdateProductAmountCommand request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request.Model.WarehouseId, request.Model.ProductId);
            var result = new CQRSResponse<ProductAmount>();

            if (productAmount == null)
            {
                result.Message = "There is no product with such Id";
                return result;
            }

            _mapper.Map(request.Model, productAmount);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = productAmount;

                return result;
            }
            else
            {
                result.Model = productAmount;
                return result;
            }
        }
    }

    public class DeleteProductAmountHandler : IRequestHandler<DeleteProductAmountCommand, CQRSResponse<ProductAmount>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteProductAmountHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductAmount>> Handle(DeleteProductAmountCommand request, CancellationToken cancellationToken)
        {
            var productAmount = await _uow.ProductAmount.GetAsync(request.Id);
            var result = new CQRSResponse<ProductAmount>();

            if (productAmount == null)
            {
                result.Message = "Didn't find this productAmount";
                return result;
            }

            _uow.ProductAmount.Delete(productAmount.Id);

            if (await _uow.SaveAsync())
            {
                result.Model = productAmount;
                result.IsSuccess = true;

                return result;
            }
            else
            {
                return result;
            }
        }
    }
}