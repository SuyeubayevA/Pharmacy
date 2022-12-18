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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IResult> Get(int Id)
        {
            var query = new GetProductTypeByIdQuery(Id);
            var result = await _mediator.Send(query);

            return result!=null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IResult> Post( ProductTypeModel model)
        {
            var command = new CreateProductTypeCommand(model);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        public async Task<IResult> Put(ProductTypeModel model)
        {
            var command = new UpdateProductTypeCommand(model);
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{productTypeId}")]
        public async Task<IResult> Delete(int productTypeId)
        {
            var command = new DeleteProductTypeCommand(productTypeId);
            return await _mediator.Send(command);
        }
    }
}
