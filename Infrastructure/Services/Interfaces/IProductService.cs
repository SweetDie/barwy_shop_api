using Infrastructure.Models.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ServiceResponse> CreateProductAsync(ProductCreateVM model);
        Task<ServiceResponse> DeleteProductAsync(Guid id);
        Task<ServiceResponse> RestoreProductAsync(Guid id);
        Task<ServiceResponse> UpdateProductAsync(ProductUpdateVM model);
    }
}
