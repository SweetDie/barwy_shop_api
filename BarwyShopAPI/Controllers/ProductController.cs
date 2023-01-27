using BarwyShopAPI.Models;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarwyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> Remove([FromBody] string id)
        {
            await _productRepository.DeleteAsync(Guid.Parse(id));
            return Ok();
        }

    }
}
