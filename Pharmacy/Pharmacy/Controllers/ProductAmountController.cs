using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Infrastructure.Commands;
using Pharmacy.Models;
using Pharmacy.Infrastructure.Queries;
using Pharmacy.API.Helpers;

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
        public async Task<IResult> GetAll()
        {
            var query = new GetAllProductAmountsQuery();
            var result = await _mediator.Send(query);
            return Helper.GetIResult(result);
        }

        [HttpGet("{Id}")]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductAmountByIdQuery(Id);
            var result = await _mediator.Send(query);

            return Helper.GetIResult(result);
        }

        [HttpPost]
        public async Task<IResult> Post( ProductAmountModel model)
        {
            var command = new CreateProductAmountCommand(model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpPut]
        public async Task<IResult> Put(int Id, ProductAmountModel model)
        {
            var command = new UpdateProductAmountCommand(model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> Delete(int id)
        {
            var command = new DeleteProductAmountCommand(id);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result); 
        }
    }
}
