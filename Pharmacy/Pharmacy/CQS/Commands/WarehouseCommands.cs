using MediatR;
using Pharmacy.Models;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Commands
{
    public class CreateWarehouseCommand : IRequest<IResult>
    {
        [Required]
        public WarehouseModel _model { get; set; }
        public CreateWarehouseCommand(WarehouseModel model)
        {
            _model = model;
        }
    }

    public class DeleteWarehouseCommand : IRequest<IResult>
    {
        [Required]
        public string _warehouseName { get; set; }

        public DeleteWarehouseCommand(string warehouseName)
        {
            _warehouseName = warehouseName;
        }
    }

    public class UpdateWarehouseCommand : IRequest<IResult>
    {
        [Required]
        public WarehouseModel _model { get; set; }

        public UpdateWarehouseCommand(WarehouseModel model)
        {
            _model = model;
        }
    }
}