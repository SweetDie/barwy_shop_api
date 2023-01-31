using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class GenericRepository<TEntity, T> : IGenericRepository<TEntity, T> where TEntity : class, IEntity<T>
    {
        private readonly AppEFContext _dbContext;

        public GenericRepository(AppEFContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result != 0;
        }

        public async Task<bool> DeleteAsync(T id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDelete = true;
                var result = await UpdateAsync(entity);
                return result;
            }
            return false;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(T id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<bool> RestoreAsync(T id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDelete = false;
                var result = await UpdateAsync(entity);
                return result;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result != 0;
        }
    }
}
