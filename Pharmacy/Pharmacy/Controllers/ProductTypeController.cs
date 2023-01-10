using AutoMapper;
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
    public class ProductTypeController: ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductTypeController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> GetAll()
        {
            var query = new GetAllProductTypesQuery();
            var result = await _mediator.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductTypeByIdQuery(Id);
            var result = await _mediator.Send(query);

            return Helper.GetIResult(result);
        }

        [HttpPost]
        public async Task<IResult> Post( ProductTypeModel model)
        {
            var command = new CreateProductTypeCommand(model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpPut]
        public async Task<IResult> Put(ProductTypeModel model)
        {
            var command = new UpdateProductTypeCommand(model);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }

        [HttpDelete]
        [Route("{productTypeId}")]
        public async Task<IResult> Delete(int productTypeId)
        {
            var command = new DeleteProductTypeCommand(productTypeId);
            var result = await _mediator.Send(command);

            return Helper.GetIResult(result);
        }
    }
}
