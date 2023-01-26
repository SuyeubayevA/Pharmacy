using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Business.CQS.Handlers.CommandsHanders.ProductType
{
    public class CreateProductTypeHandler : IRequestHandler<CreateProductTypeCommand, Unit>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateProductTypeHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.ProductType.GetAsync(request.Model.Name) != null)
            {
                throw new Exception("The object already exist !");
            }

            var productType = _mapper.Map<Domain.Core.ProductType>(request.Model);
            _uow.ProductType.Create(productType);

            await _uow.SaveAsync();

            return Unit.Value;
        }
    }
}