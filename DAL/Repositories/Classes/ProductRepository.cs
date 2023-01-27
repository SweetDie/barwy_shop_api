using DAL.Entities.Products;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product, Guid>,
        IProductRepository
    {
        public ProductRepository(AppEFContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Product> Products => GetAll();
    }
}
