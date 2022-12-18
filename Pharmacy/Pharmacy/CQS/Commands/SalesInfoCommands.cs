using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public class CreateSalesInfoCommand : IRequest<IResult>
    {
        [Required]
        public SalesInfoModel _model { get; set; }
        public CreateSalesInfoCommand(SalesInfoModel model)
        {
            _model = model;
        }
    }

    public class DeleteSalesInfoCommand : IRequest<IResult>
    {
        [Required]
        public int _productId { get; set; }

        public DeleteSalesInfoCommand(int productId)
        {
            _productId = productId;
        }
    }

    public class UpdateSalesInfoCommand : IRequest<IResult>
    {
        [Required]
        public SalesInfoModel _model { get; set; }

        public UpdateSalesInfoCommand(SalesInfoModel model)
        {
            _model = model;
        }
    }
}