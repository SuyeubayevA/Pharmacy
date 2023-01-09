using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateProductTypeCommand(ProductTypeModel Model) : IRequest<IResult> { }

    public record DeleteProductTypeCommand(int Id) : IRequest<IResult> { }

    public record UpdateProductTypeCommand(ProductTypeModel Model) : IRequest<IResult> { }
}