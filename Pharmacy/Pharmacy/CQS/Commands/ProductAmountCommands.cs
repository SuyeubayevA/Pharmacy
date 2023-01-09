using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public record CreateProductAmountCommand(ProductAmountModel Model) : IRequest<IResult> { }

    public record DeleteProductAmountCommand(int Id) : IRequest<IResult> { }

    public record UpdateProductAmountCommand(ProductAmountModel Model) : IRequest<IResult> { }
}