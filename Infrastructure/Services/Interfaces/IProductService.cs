using Infrastructure.Models.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ServiceResponse> CreateProductAsync(ProductCreateVM model);
    }
}
