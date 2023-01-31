using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        IQueryable<Product> Products { get; }
        Task<bool> AddToCategoryAsync(Product product, string categoryName);
        Task<bool> AddToCategoryAsync(Product product, IEnumerable<string> categories);
    }
}
