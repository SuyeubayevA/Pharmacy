using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateSalesInfoHandler : IRequestHandler<CreateSalesInfoCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.SalesInfo.GetAsync(request.Model.ProductId, 0) != null)
            {
                throw new Exception("The object already exist !");
            }

            var salesInfo = _mapper.Map<SalesInfo>(request.Model);
            _uow.SalesInfo.Create(salesInfo);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }

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

    public class DeleteSalesInfoHandler : IRequestHandler<DeleteSalesInfoCommand, Unit>
    {
        private readonly UnitOfWork _uow;

        public DeleteSalesInfoHandler(UnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Unit> Handle(DeleteSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request.ProductId, 0);

            _uow.SalesInfo.Delete(salesInfo.Id);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}