using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductType
{
    public class UpdateProductTypeHandler : IRequestHandler<UpdateProductTypeCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var productType = await _uow.ProductType.GetAsync(request.Model.Id);
            _mapper.Map(request.Model, productType);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}