using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public record CreateProductCommand(ProductModel Model) : IRequest<IResult> { }

    public record DeleteProductCommand(string ProductName) : IRequest<IResult> { }

    public record UpdateProductCommand(int Id, ProductModel Model) : IRequest<IResult> { }

    public record UpdateProductsWarehouseCommand(int Id, ProductModel Model, int WarehouseId, int Amount, float Discount) : IRequest<IResult> { }
}