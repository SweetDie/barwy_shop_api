using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product, Guid>,
        IProductRepository
    {

        public ProductRepository(AppEFContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Product> Products => GetAll().Where(p => p.IsDelete == false).Include(p => p.Categories);

        public async Task AddCategoryAsync(Product product, Category category)
        {
            
        }
    }
}
