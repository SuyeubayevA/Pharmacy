using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Business.CQS;
using Pharmacy.Models;

namespace Pharmacy.Infrastructure.Commands
{
    public record CreateSalesInfoCommand(SalesInfoModel Model) : IRequest<CQRSResponse<SalesInfo>> { }

    public record DeleteSalesInfoCommand(int ProductId) : IRequest<CQRSResponse<SalesInfo>> { }

    public record UpdateSalesInfoCommand(SalesInfoModel Model) : IRequest<CQRSResponse<SalesInfo>> { }
}