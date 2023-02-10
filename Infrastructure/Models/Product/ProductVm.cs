using Infrastructure.Models.Category;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Models.Product
{
    public sealed class ProductVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }

        public ICollection<CategoryVm> Categories { get; set; }
    }
}
