using AutoMapper;
using MediatR;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Business.CQS;

namespace Pharmacy.Infrastructure.Handlers.CommandsHanders
{
    public class CreateSalesInfoHandler : IRequestHandler<CreateSalesInfoCommand, CQRSResponse<SalesInfo>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<SalesInfo>> Handle(CreateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var result = new CQRSResponse<SalesInfo>();

            if (await _uow.SalesInfo.GetAsync(request.Model.ProductId, 0) != null)
            {
                result.Message = "The object already exist !";

                return result;
            }

            var salesInfo = _mapper.Map<SalesInfo>(request.Model);
            _uow.SalesInfo.Create(salesInfo);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = salesInfo;

                return result;
            }

            result.Model = salesInfo;
            return result;
        }
    }

    public class UpdateSalesInfoHandler : IRequestHandler<UpdateSalesInfoCommand, CQRSResponse<SalesInfo>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public UpdateSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<SalesInfo>> Handle(UpdateSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request.Model.ProductId);
            var result = new CQRSResponse<SalesInfo>();

            if (salesInfo == null)
            {
                result.Message = "There is no SalesInfo with such Id";
                return result;
            }

            _mapper.Map(request.Model, salesInfo);

            if (await _uow.SaveAsync())
            {
                result.IsSuccess = true;
                result.Model = salesInfo;

                return result;
            }
            else
            {
                result.Model = salesInfo;
                return result;
            }
        }
    }

    public class DeleteSalesInfoHandler : IRequestHandler<DeleteSalesInfoCommand, CQRSResponse<SalesInfo>>
    {
        private readonly UnitOfWork _uow;
        private readonly IMapper _mapper;

        public DeleteSalesInfoHandler(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CQRSResponse<SalesInfo>> Handle(DeleteSalesInfoCommand request, CancellationToken cancellationToken)
        {
            var salesInfo = await _uow.SalesInfo.GetAsync(request.ProductId, 0);
            var result = new CQRSResponse<SalesInfo>();

            if (salesInfo == null)
            {
                result.Message = "Didn't find this SalesInfo";
                return result;
            }

            _uow.SalesInfo.Delete(salesInfo.Id);

            if (await _uow.SaveAsync())
            {
                result.Model = salesInfo;
                result.IsSuccess = true;

                return result;
            }
            else
            {
                return result;
            }
        }
    }
}