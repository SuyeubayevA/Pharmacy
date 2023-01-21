using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateProductAmountCommand(ProductAmountModel Model) : IRequest<Unit> { }

    public record DeleteProductAmountCommand(int Id) : IRequest<Unit> { }

    public record UpdateProductAmountCommand(ProductAmountModel Model) : IRequest<Unit> { }
}