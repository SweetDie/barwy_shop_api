using DAL.Entities;
using Infrastructure.Models.Category;

namespace Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CategoryCreateVM model);
        Task<List<CategoryVM>> GetAllAsync();
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> RestoreAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(CategoryUpdateVM model);
    }
}
