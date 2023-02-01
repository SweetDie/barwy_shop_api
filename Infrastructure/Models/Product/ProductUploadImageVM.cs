using Microsoft.AspNetCore.Http;

namespace Infrastructure.Models.Product
{
    public class ProductUploadImageVM
    {
        public Guid ProductId { get; set; }
        public IFormFile Image { get; set; }
    }
}
