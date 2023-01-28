using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category, Guid>,
        ICategoryRepository
    {
        public CategoryRepository(AppEFContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Category> Categories => GetAll().Where(c => c.IsDelete == false).Include(c => c.Products);

        public async Task<Category> GetByNameAsync(string name)
        {
            var result = await Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
            return result;
        }
    }
}
