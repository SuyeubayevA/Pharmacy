using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Commands;
using Pharmacy.Models;
using Pharmacy.Queries;

namespace Pharmacy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            if(result != null)
            {
                return Results.Ok(result);
            }
            else
            {
                return Results.NotFound("There is no any product");
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result!=null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post( ProductModel model)
        {
            var command = new CreateProductCommand(model);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IResult> Put(int Id, ProductModel model)
        {
            var command = new UpdateProductCommand(Id, model);
            return await _mediator.Send(command);
        }

        [HttpPut("AddWarehouse")]
        public async Task<IResult> PutWarehouse(int Id, ProductModel model, int warehouseId, int amount = 0, float discount = 0)
        {
            var command = new UpdateProductsWarehouseCommand(Id, model, warehouseId, amount, discount);
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{productName}")]
        public async Task<IResult> Delete(string productName)
        {
            var command = new DeleteProductCommand(productName);
            return await _mediator.Send(command);
        }
    }
}
