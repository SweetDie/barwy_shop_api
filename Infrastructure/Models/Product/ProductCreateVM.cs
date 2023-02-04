using Infrastructure.Models.Category;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Models.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public IFormFile Image { get; set; }

        public virtual ICollection<string> Categories { get; set; }
    }
}
