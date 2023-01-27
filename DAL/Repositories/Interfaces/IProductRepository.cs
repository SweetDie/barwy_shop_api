using DAL.Entities.Products;

namespace DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, Guid>
    {
        IQueryable<Product> Products { get; }
    }
}
