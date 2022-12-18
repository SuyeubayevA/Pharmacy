
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Queries;

namespace Pharmacy.Handlers.ProductQueriesHanders
{
    public class GetSalesInfoByIdHandler : IRequestHandler<GetSalesInfoByIdQuery, SalesInfo>
    {
        private readonly UnitOfWork _uow;

        public GetSalesInfoByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<SalesInfo> Handle(GetSalesInfoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.SalesInfo.GetAsync(request._id);
        }
    }

    public class GetAllSalesInfoHandler : IRequestHandler<GetAllSalesInfosQuery, SalesInfo[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllSalesInfoHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<SalesInfo[]> Handle(GetAllSalesInfosQuery request, CancellationToken cancellationToken)
        {
            return await _uow.SalesInfo.GetAllASync();
        }
    }
}