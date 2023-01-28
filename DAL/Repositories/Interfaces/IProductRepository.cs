using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        IQueryable<Product> Products { get; }
        Task AddCategoryAsync(Product product, Category category);
    }
}
