using DAL.Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryByNameAsync(string name);
    }
}
