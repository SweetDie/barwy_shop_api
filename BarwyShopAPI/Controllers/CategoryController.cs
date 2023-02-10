using DAL.Repositories.Classes;
using Infrastructure.Models.Category;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarwyShopAPI.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateVm model)
        {
            var result = await _categoryService.CreateAsync(model);
            return SendResponse(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateVm model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return SendResponse(result);
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] string id)
        {
            var result = await _categoryService.DeleteAsync(Guid.Parse(id));
            return SendResponse(result);
        }
        
        [HttpPost("restore")]
        public async Task<IActionResult> RestoreAsync([FromBody] string id)
        {
            var result = await _categoryService.RestoreAsync(Guid.Parse(id));
            return SendResponse(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
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