using AutoMapper;
using Pharmacy.Domain.Core;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Models;
using System.Web.Http;

namespace Pharmacy.Controllers
{
    [RoutePrefix("api/pharm")]
    public class ProductController: ApiController
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(UnitOfWork uow, IMapper mapper)
        {
            unitOfWork = uow;
            _mapper = mapper;
        }

        [Route("{Id:int}")]
        public async Task<IHttpActionResult> Get(int Id = 0)
        {
            try
            {
                if (Id == 0)
                {
                    var result = await unitOfWork.Product.GetAllASync();
                    return Ok(result);
                }
                else
                {
                    var result = await unitOfWork.Product.GetAsync(Id);
                    return Ok(result);
                }
            }
            catch 
            {
                return InternalServerError();            
            }
        }

        public async Task<IHttpActionResult> Post( ProductModel model)
        {
            try
            {
                if (await unitOfWork.Product.GetAsync(model.Name) != null)
                {
                    ModelState.AddModelError("Product Name", "The product with the same name alredy exists!");
                }

                if (ModelState.IsValid)
                {
                    var product = _mapper.Map<Product>(model);
                    unitOfWork.Product.Create(product);

                    if(await unitOfWork.SaveAsync())
                    {
                        return Ok(product);
                    }
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }

        public async Task<IHttpActionResult> Put(int Id, ProductModel model)
        {
            try
            {
                var product = unitOfWork.Product.GetAsync(Id);

                if(product == null) { return NotFound(); }

                await _mapper.Map(model, product);

                if( await unitOfWork.SaveAsync())
                {
                    return Ok(product);
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("{productName}")]
        public async Task<IHttpActionResult> Delete(string productName)
        {
            try
            {
                var product = unitOfWork.Product.GetAsync(productName);

                if (product == null) { return NotFound(); }

                unitOfWork.Product.Delete(product.Id);

                if(await unitOfWork.SaveAsync())
                {
                    return Ok();
                }
                else
                {
                    return InternalServerError();
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
