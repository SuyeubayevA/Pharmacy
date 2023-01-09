using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateWarehouseCommand(WarehouseModel Model) : IRequest<IResult> { }

    public record DeleteWarehouseCommand(string WarehouseName) : IRequest<IResult> { }

    public record UpdateWarehouseCommand(WarehouseModel Model) : IRequest<IResult> { }
}