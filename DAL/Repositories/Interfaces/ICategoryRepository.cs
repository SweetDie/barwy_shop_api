using DAL.Entities.Products;

namespace DAL.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, Guid>
    {
        IQueryable<Category> Categories { get; }
    }
}
