using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Commands;
using Pharmacy.Models;
using Pharmacy.Queries;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAmountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductAmountController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllProductAmountsQuery();
            var result = await _mediator.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductAmountByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result!=null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post( ProductAmountModel model)
        {
            var command = new CreateProductAmountCommand(model);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IResult> Put(int Id, ProductAmountModel model)
        {
            var command = new UpdateProductAmountCommand(model);
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var command = new DeleteProductAmountCommand(id);
            return await _mediator.Send(command);
        }
    }
}
