using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public class CreateProductAmountCommand : IRequest<IResult>
    {
        [Required]
        public ProductAmountModel _model { get; set; }
        public CreateProductAmountCommand(ProductAmountModel model)
        {
            _model = model;
        }
    }

    public class DeleteProductAmountCommand : IRequest<IResult>
    {
        [Required]
        public int _Id { get; set; }

        public DeleteProductAmountCommand(int id)
        {
            _Id = id;
        }
    }

    public class UpdateProductAmountCommand : IRequest<IResult>
    {
        [Required]
        public ProductAmountModel _model { get; set; }

        public UpdateProductAmountCommand(ProductAmountModel model)
        {
            _model = model;
        }
    }
}