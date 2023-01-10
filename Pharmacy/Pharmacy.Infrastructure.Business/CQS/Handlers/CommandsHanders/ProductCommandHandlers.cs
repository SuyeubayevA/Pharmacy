using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using MediatR;
using AutoMapper;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CQRSResponse<Product>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<Product>();

            if (await _uow.Product.GetAsync(request.Model.Name) != null)
            {
                result.Message = "The object already exist !";

                return result;
            }


            var product = _mapper.Map<Product>(request.Model);
            _uow.Product.Create(product);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = product;

                return result;
            }

            result.Model = product;
            return result;
        }
    }

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, CQRSResponse<Product>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.Id);
            var result = new CQRSResponse<Product>();

            if (product == null) 
            { 
                result.Message = "There is no product with suh Id";
                return result;
            }

            _mapper.Map(request.Model, product);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = product;

                return result;
            }
            else
            {
                result.Model = product;
                return result;
            }
        }
    }

    public class UpdateProductsWarehouseHandler : IRequestHandler<UpdateProductsWarehouseCommand, CQRSResponse<bool>>
    {
        private readonly UnitOfWork _uow;

        public UpdateProductsWarehouseHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<CQRSResponse<bool>> Handle(UpdateProductsWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouseLinkUpdated = _uow.Product.UpdateWarehouseLink(request.Id, request.WarehouseId, request.Amount, request.Discount);
            var result = new CQRSResponse<bool>();

            if (warehouseLinkUpdated && await _uow.SaveAsync())
            {
                result.Model = warehouseLinkUpdated;
                result.IsSuccess = true;

                return result;
            }
            else
            {
                return result;
            }
        }
    }

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, CQRSResponse<Product>>
    {
        private readonly UnitOfWork _uow;

        public DeleteProductHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<CQRSResponse<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.Product.GetAsync(request.ProductName);
            var result = new CQRSResponse<Product>();

            if (product == null) 
            {
                result.Message = "Didn't find this product";
                return result;
            }

            _uow.Product.Delete(product.Id);

            if (await _uow.SaveAsync())
            {
                result.Model = product;
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