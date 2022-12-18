using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public class CreateProductTypeCommand : IRequest<IResult>
    {
        [Required]
        public ProductTypeModel _model { get; set; }
        public CreateProductTypeCommand(ProductTypeModel model)
        {
            _model = model;
        }
    }

    public class DeleteProductTypeCommand : IRequest<IResult>
    {
        [Required]
        public int _Id { get; set; }

        public DeleteProductTypeCommand(int id)
        {
            _Id = id;
        }
    }

    public class UpdateProductTypeCommand : IRequest<IResult>
    {
        [Required]
        public ProductTypeModel _model { get; set; }

        public UpdateProductTypeCommand(ProductTypeModel model)
        {
            _model = model;
        }
    }
}