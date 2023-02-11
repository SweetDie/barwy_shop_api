using Infrastructure.Models.Product;
using Infrastructure.Services;
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
        public async Task<IActionResult> CreateAsync([FromForm] ProductCreateVm model)
        {
            var result = await _productService.CreateAsync(model);
            return SendResponse(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductUpdateVm model)
        {
            var result = await _productService.UpdateAsync(model);
            return SendResponse(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] string id)
        {
            var result = await _productService.DeleteAsync(Guid.Parse(id));
            return SendResponse(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            return SendResponse(result);
        }

        [HttpGet("getAllByCategory")]
        public async Task<IActionResult> GetAllByCategoryAsync(string categoryName)
        {
            var result = await _productService.GetAllByCategoryAsync(categoryName);
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
