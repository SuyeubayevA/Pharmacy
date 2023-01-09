using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateSalesInfoCommand(SalesInfoModel Model) : IRequest<IResult> { }

    public record DeleteSalesInfoCommand(int ProductId) : IRequest<IResult> { }

    public record UpdateSalesInfoCommand(SalesInfoModel Model) : IRequest<IResult> { }
}