using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, Guid>
    {
        IQueryable<Category> Categories { get; }
        Task<Category> GetByNameAsync(string name);
    }
}
