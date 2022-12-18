using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public class CreateProductCommand : IRequest<IResult>
    {
        [Required]
        public ProductModel _model { get; set; }
        public CreateProductCommand(ProductModel model)
        {
            _model = model;
        }
    }

    public class DeleteProductCommand : IRequest<IResult>
    {
        [Required]
        public string _productName { get; set; }

        public DeleteProductCommand(string productName)
        {
            _productName = productName;
        }
    }

    public class UpdateProductCommand : IRequest<IResult>
    {
        [Required]
        public ProductModel _model { get; set; }

        [Required]
        public int _Id { get; set; }

        public UpdateProductCommand(int Id, ProductModel model)
        {
            _model = model;
            _Id = Id;
        }
    }
}