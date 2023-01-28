using DAL.Entities;
using DAL.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var result = await _categoryRepository.Categories.FirstOrDefaultAsync(c => c.Name == name);
            return result;
        }
    }
}
