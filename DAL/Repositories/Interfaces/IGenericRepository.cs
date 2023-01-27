using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(T id);

        Task CreateAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(T id);
    }
}
