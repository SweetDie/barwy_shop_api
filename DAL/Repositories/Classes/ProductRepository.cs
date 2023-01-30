using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product, Guid>,
        IProductRepository
    {
        private readonly AppEFContext _dbContext;

        public ProductRepository(AppEFContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => GetAll().Where(p => p.IsDelete == false).Include(p => p.CategoryProduct);

        public async Task<bool> AddToCategoryAsync(Product product, string categoryName)
        {
            var category = await _dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.NormalizedName == categoryName.ToUpper());
            if (category == null)
            {
                return false;
            }
            var categoryProduct = new CategoryProduct
            {
                Category = category,
                Product = product
            };
            _dbContext.Entry(product).State = EntityState.Unchanged;
            _dbContext.Entry(category).State = EntityState.Unchanged;
            _dbContext.Entry(product).State = EntityState.Added;
            var result = await _dbContext.SaveChangesAsync();
            return result == 0 ? false : true;
        }

        public async Task<bool> CreateProductAsync(Product product, ICollection<string> categories)
        {
            var createResult = await CreateAsync(product);
            if(createResult)
            {
                foreach (var category in categories)
                {
                    var resultAddToCategory = await AddToCategoryAsync(product, category);
                    if(!resultAddToCategory)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
