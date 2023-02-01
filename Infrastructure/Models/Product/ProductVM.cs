using Infrastructure.Models.Category;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Models.Product
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public virtual ICollection<CategoryVM> Categories { get; set; }
    }
}
