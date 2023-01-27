using DAL.Entities.Products;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category, Guid>,
        ICategoryRepository
    {
        public CategoryRepository(AppEFContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Category> Categories => GetAll();
    }
}
