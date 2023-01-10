using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Models;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateProductTypeCommand(ProductTypeModel Model) : IRequest<CQRSResponse<ProductType>> { }

    public record DeleteProductTypeCommand(int Id) : IRequest<CQRSResponse<ProductType>> { }

    public record UpdateProductTypeCommand(ProductTypeModel Model) : IRequest<CQRSResponse<ProductType>> { }
}