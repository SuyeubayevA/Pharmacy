using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Models;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateSalesInfoCommand(SalesInfoModel Model) : IRequest<Unit> { }

    public record DeleteSalesInfoCommand(int ProductId) : IRequest<Unit> { }

    public record UpdateSalesInfoCommand(SalesInfoModel Model) : IRequest<Unit> { }
}