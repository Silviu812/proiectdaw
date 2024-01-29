using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Services;
using System.Security.Claims;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = WebRoles.Member)]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Policy = WebRoles.Admin)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            var createdProduct = await _productService.CreateProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = WebRoles.Admin)]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct == null) return NotFound("Product not found");
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = WebRoles.Admin)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProduct(id);
            if (!success) return NotFound("Product not found");
            return NoContent();
        }
    }
}
