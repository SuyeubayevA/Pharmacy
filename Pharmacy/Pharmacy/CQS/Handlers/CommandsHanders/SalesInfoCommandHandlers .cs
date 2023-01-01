using AutoMapper;
using MediatR;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;

namespace Pharmacy.Handlers.CommandsHanders
{
    public class CreateSalesInfoHandler : IRequestHandler<CreateSalesInfoCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(CreateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            if (await _uow.SalesInfo.GetAsync(request._model.ProductId, 0) != null)
            {
                Results.BadRequest("The object already exist !");
            }

            if (request._model is SalesInfoModel)
            {
                var salesInfo = _mapper.Map<SalesInfo>(request._model);
                _uow.SalesInfo.Create(salesInfo);

                if (await _uow.SaveAsync())
                {
                    return Results.Ok(salesInfo);
                }
            }

            return Results.BadRequest(request._model);
        }
    }

    public class UpdateSalesInfoHandler : IRequestHandler<UpdateSalesInfoCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(UpdateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request._model.ProductId);

            if (salesInfo == null) { return Results.NotFound(); }

            _mapper.Map(request._model, salesInfo);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(salesInfo);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }

    public class DeleteSalesInfoHandler : IRequestHandler<DeleteSalesInfoCommand, IResult>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResult> Handle(DeleteSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request._productId, 0);

            if (salesInfo == null) { return Results.NotFound(); }

            _uow.SalesInfo.Delete(salesInfo.Id);

            if (await _uow.SaveAsync())
            {
                return Results.Ok(salesInfo);
            }
            else
            {
                return Results.StatusCode(500);
            }
        }
    }
}