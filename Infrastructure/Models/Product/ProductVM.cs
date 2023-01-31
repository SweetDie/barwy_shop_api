using Infrastructure.Models.Category;

namespace Infrastructure.Models.Product
{
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<CategoryVM>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public virtual IEnumerable<CategoryVM> Categories { get; set; }
    }
}
