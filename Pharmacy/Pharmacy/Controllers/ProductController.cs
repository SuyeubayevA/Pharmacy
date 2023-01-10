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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);

            return Helper.GetIResult(result);
        }

        [HttpGet("{Id}")]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductByIdQuery(Id);
            var result = await _mediator.Send(query);

            return Helper.GetIResult(result);
        }

        [HttpPost]
        public async Task<IResult> Post( ProductModel model)
        {
            var command = new CreateProductCommand(model);
            var result = await _mediator.Send(command);
            return Helper.GetIResult(result);
        }

        [HttpPut]
        public async Task<IResult> Put(int Id, ProductModel model)
        {
            var command = new UpdateProductCommand(Id, model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpPut("AddWarehouse")]
        public async Task<IResult> PutWarehouse(int Id, ProductModel model, int warehouseId, int amount = 0, float discount = 0)
        {
            var command = new UpdateProductsWarehouseCommand(Id, model, warehouseId, amount, discount);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpDelete]
        [Route("{productName}")]
        public async Task<IResult> Delete(string productName)
        {
            var command = new DeleteProductCommand(productName);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }
    }
}
