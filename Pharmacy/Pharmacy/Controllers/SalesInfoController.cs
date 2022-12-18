using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Commands;
using Pharmacy.Models;
using Pharmacy.Queries;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesInfoController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllSalesInfosQuery();
            var result = await _mediator.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetSalesInfoByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result!=null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post( SalesInfoModel model)
        {
            var command = new CreateSalesInfoCommand(model);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IResult> Put(int Id, SalesInfoModel model)
        {
            var command = new UpdateSalesInfoCommand(model);
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{productName}")]
        public async Task<IResult> Delete(int productId)
        {
            var command = new DeleteSalesInfoCommand(productId);
            return await _mediator.Send(command);
        }
    }
}
