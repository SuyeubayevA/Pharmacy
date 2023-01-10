using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, CQRSResponse<ProductType>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductType>> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<ProductType>();

            if (await _uow.ProductType.GetAsync(request.Model.Name) != null)
            {
                result.Message = "The object already exist !";

                return result;
            }

            var productType = _mapper.Map<ProductType>(request.Model);
            _uow.ProductType.Create(productType);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = productType;

                return result;
            }

            result.Model = productType;
            return result;
        }
    }

    public class UpdateProductTypeHandler : IRequestHandler<UpdateProductTypeCommand, CQRSResponse<ProductType>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductType>> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Model.Id);
            var result = new CQRSResponse<ProductType>();

            if (productType == null)
            {
                result.Message = "There is no productType with such Id";
                return result;
            }

            _mapper.Map(request.Model, productType);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = productType;

                return result;
            }
            else
            {
                result.Model = productType;
                return result;
            }
        }
    }

    public class DeleteProductTypeHandler : IRequestHandler<DeleteProductTypeCommand, CQRSResponse<ProductType>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<ProductType>> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Id);
            var result = new CQRSResponse<ProductType>();

            if (productType == null)
            {
                result.Message = "Didn't find this productType";
                return result;
            }

            _uow.ProductType.Delete(productType.Id);

            if (await _uow.SaveAsync())
            {
                result.Model = productType;
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