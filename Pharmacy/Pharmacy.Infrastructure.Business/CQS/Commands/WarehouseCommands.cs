using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateWarehouseCommand(WarehouseModel Model) : IRequest<Unit> { }

    public record DeleteWarehouseCommand(string WarehouseName) : IRequest<Unit> { }

    public record UpdateWarehouseCommand(WarehouseModel Model) : IRequest<Unit> { }
}