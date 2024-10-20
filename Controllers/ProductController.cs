
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petstore.Data.Object;
using Server.Context;
using Server.DTO;
using Server.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DBContext dbContext;

        public ProductController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO dto)
        {
            try
            {
                Response objResponse = new Response();
                ProductEntity productEntity = new ProductEntity();
                productEntity.Brand = dto.Brand;
                productEntity.Title = dto.Title;

                await dbContext.ProductEntities.AddAsync(productEntity);
                await dbContext.SaveChangesAsync();


                objResponse.Message = "";
                objResponse.Data = "SuccessFully created Data";
                objResponse.success = true;


                return Ok(objResponse);
            }
            catch (Exception ex)
            {
                Response objResponse = new Response();
                objResponse.Message = ex.Message;
                objResponse.Data = "";
                objResponse.success = false;
                return BadRequest(objResponse);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetProductList()
        {
            try
            {
                var productArr = await dbContext.ProductEntities.ToListAsync();

                return Ok(productArr);
               
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductById([FromRoute] long id)
        {
            try
            {
                var productObj = await dbContext.ProductEntities.FirstOrDefaultAsync(q => q.Id == id);

                if (productObj is null)
                {
                    return NotFound("Product not found");
                }

                return Ok(productObj);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] ProductDTO dto)
        {
            try
            {
                var productObj = await dbContext.ProductEntities.FirstOrDefaultAsync(q => q.Id == id);

                if (productObj is null)
                {
                    return NotFound("Product not found");
                }

                productObj.Title = dto.Title;

                productObj.Brand = dto.Brand;

                productObj.updatedAt = DateTime.Now;

                await dbContext.SaveChangesAsync();

                return Ok("Successfully updated product");
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            try
            {
                var productObj = await dbContext.ProductEntities.FirstOrDefaultAsync(q => q.Id == id);

                if (productObj is null)
                {
                    return NotFound("Product not found");
                }

                 dbContext.ProductEntities.Remove(productObj);

                await dbContext.SaveChangesAsync();

                return Ok("Successfully deleted product");
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
