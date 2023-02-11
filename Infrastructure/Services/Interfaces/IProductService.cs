using Infrastructure.Models.Product;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> CreateAsync(ProductCreateVm model);
        Task<ServiceResponse> DeleteAsync(Guid id);
        Task<ServiceResponse> UpdateAsync(ProductUpdateVm model);
        Task<ServiceResponse> GetAllByCategoryAsync(string categoryName);
    }
}
