using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using serverApp.Models;
using serverApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serverApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _context;

        public ProductController(IProductRepository context)
        {
            _context = context;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.GetAllProduct();
            return Ok(products);
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.GetProduct(id);
            if(product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }

        }
        [HttpPost("add")]
        public async Task<IActionResult> AddProduct([FromBody] ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var Id = await _context.AddProduct(model);
                return CreatedAtAction(nameof(GetProduct), new { id = Id, controller = "Product" }, Id);

            }
            else
            {
                return BadRequest();
            }
         
        }
        [HttpPut("add/{id}")]
        public async Task<IActionResult> UpdateProduct(int Id,ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _context.UpdateProductAsync(Id, model);
                if (response > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new { response});
                }
            }
            else
            {
                return BadRequest();
            }

           
        }
        [HttpDelete("rm/{id}")]
        public async Task<IActionResult> RemoveProduct([FromRoute] int id)
        {
            var response = await _context.ProductRemoveAsync(id);
            if (response > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
