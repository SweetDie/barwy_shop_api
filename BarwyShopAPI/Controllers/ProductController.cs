using DAL.Repositories.Interfaces;
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
            var result = await _productService.CreateProductAsync(model);
            return SendResponse(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
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
