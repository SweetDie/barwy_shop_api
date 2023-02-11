using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category, Guid>,
        ICategoryRepository
    {
        private readonly AppEFContext _dbContext;

        public CategoryRepository(AppEFContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> Categories => GetAll().Include(c => c.CategoryProduct);

        public async Task CreateCategoryAsync(Category category)
        {
            category.NormalizedName = category.Name.ToUpper();
            category.DateCreated = DateTime.Now.ToUniversalTime();
            await CreateAsync(category);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            var result = await Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name);
            return result;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            var result = await _dbContext.Categories.FirstOrDefaultAsync(c => c.NormalizedName == name.ToUpper());
            return result != null;
        }
    }
}
