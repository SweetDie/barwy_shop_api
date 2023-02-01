using Infrastructure.Models.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> CreateAsync(ProductCreateVM model);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> RestoreAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(ProductUpdateVM model);
        Task<ServiceResponse> GetAllByCategoryAsync(string categoryName);
        Task<ServiceResponse> UploadImageAsync(ProductUploadImageVM model);
    }
}
