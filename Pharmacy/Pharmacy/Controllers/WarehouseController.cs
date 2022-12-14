using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.API.Helpers;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController: ControllerBase
    {
        private readonly IMediator _mediator;

        public WarehouseController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllWarehousesQuery();
            var result = await _mediator.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetWarehouseByIdQuery(Id);
            var result = await _mediator.Send(query);

            return Helper.GetIResult(result);
        }

        [HttpPost]
        public async Task<IResult> Post( WarehouseModel model)
        {
            var command = new CreateWarehouseCommand(model);
            var result = await _mediator.Send(command);
            return Helper.GetIResult(result);
        }

        [HttpPut]
        public async Task<IResult> Put(WarehouseModel model)
        {
            var command = new UpdateWarehouseCommand(model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpDelete]
        [Route("{warehouseName}")]
        public async Task<IResult> Delete(string warehouseName)
        {
            var command = new DeleteWarehouseCommand(warehouseName);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }
    }
}
