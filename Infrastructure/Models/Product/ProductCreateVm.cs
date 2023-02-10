using Infrastructure.Models.Category;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Models.Product
{
    public sealed class ProductCreateVm
    {
        public string Name { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public IFormFile Image { get; set; }

        public ICollection<string> Categories { get; set; }
    }
}
