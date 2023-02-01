using Infrastructure.Models.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllAsync();
        Task<ServiceResponse> CreateAsync(ProductCreateVM model);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> RestoreAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(ProductUpdateVM model);
    }
}
