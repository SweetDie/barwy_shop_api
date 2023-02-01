using Infrastructure;
using Infrastructure.Models.Product;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarwyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductCreateVM model)
        {
            var result = await _productService.CreateAsync(model);
            return SendResponse(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductUpdateVM model)
        {
            var products = await _productService.UpdateAsync(model);
            return Ok(products);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteProductAsync([FromBody] string id)
        {
            var result = await _productService.DeleteAsync(Guid.Parse(id));
            return SendResponse(result);
        }

        [HttpPost("restore")]
        public async Task<IActionResult> RestoreProductAsync([FromBody] string id)
        {
            var result = await _productService.RestoreAsync(Guid.Parse(id));
            return SendResponse(result);
        }

        private IActionResult SendResponse(ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
