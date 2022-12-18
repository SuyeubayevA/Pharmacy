using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Commands;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using Pharmacy.Queries;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllWarehousesQuery();
            var result = await _mediator.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetWarehouseByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result!=null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post( WarehouseModel model)
        {
            var command = new CreateWarehouseCommand(model);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IResult> Put(WarehouseModel model)
        {
            var command = new UpdateWarehouseCommand(model);
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{warehouseName}")]
        public async Task<IResult> Delete(string warehouseName)
        {
            var command = new DeleteWarehouseCommand(warehouseName);
            return await _mediator.Send(command);
        }
    }
}
