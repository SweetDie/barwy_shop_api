using Infrastructure.Models.Product;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ServiceResponse> CreateProductAsync(ProductCreateVM model);
        public Task<ServiceResponse> AddCategoryToProductAsync(string category);
        public Task<ServiceResponse> DeleteProductAsync(Guid id);
    }
}
