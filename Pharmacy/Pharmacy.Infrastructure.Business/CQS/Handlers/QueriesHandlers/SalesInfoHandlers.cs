
using MediatR;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.DTO;
using Pharmacy.Queries;

namespace Pharmacy.Infrastructure.Handlers.ProductQueriesHanders
{
    public class GetSalesInfoByIdHandler : IRequestHandler<GetSalesInfoByIdQuery, SalesInfoDetailsDTO>
    {
        private readonly UnitOfWork _uow;

        public GetSalesInfoByIdHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<SalesInfoDetailsDTO> Handle(GetSalesInfoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.SalesInfo.GetAsync(request._id);
        }
    }

    public class GetAllSalesInfoHandler : IRequestHandler<GetAllSalesInfosQuery, SalesInfoDTO[]>
    {
        private readonly UnitOfWork _uow;

        public GetAllSalesInfoHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<SalesInfoDTO[]> Handle(GetAllSalesInfosQuery request, CancellationToken cancellationToken)
        {
            return await _uow.SalesInfo.GetAllASync();
        }
    }
}