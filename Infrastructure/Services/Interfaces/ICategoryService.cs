using DAL.Entities;
using Infrastructure.Models.Category;

namespace Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> CreateAsync(CategoryCreateVm model);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(CategoryUpdateVm model);
    }
}
