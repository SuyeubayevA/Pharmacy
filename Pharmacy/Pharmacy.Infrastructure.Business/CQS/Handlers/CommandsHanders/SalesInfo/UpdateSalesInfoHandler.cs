using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.SalesInfo
{
    public class UpdateSalesInfoHandler : IRequestHandler<UpdateSalesInfoCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request.Model.ProductId);

            _mapper.Map(request.Model, salesInfo);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}