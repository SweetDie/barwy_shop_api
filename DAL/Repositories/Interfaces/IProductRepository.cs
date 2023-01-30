using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        IQueryable<Product> Products { get; }
        Task<bool> AddToCategoryAsync(Product product, string categoryName);
        Task<bool> CreateProductAsync(Product product, ICollection<string> categories);
    }
}
