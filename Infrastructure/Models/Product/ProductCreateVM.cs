using Infrastructure.Models.Category;

namespace Infrastructure.Models.Product
{
    public class ProductCreateVM
    {
        public ProductCreateVM()
        {
            Categories = new List<string>();
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public virtual IEnumerable<string> Categories { get; set; }
    }
}
