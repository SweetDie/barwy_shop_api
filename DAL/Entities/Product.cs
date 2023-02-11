using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Image;

namespace DAL.Entities
{
    public sealed class Product : BaseEntity<Guid>
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Article { get; set; }
        public decimal Price { get; set; }
        [MaxLength(20)]
        public string Size { get; set; }
        
        public Guid ImageId { get; set; }
        public ProductImageEntity Image { get; set; }
        public ICollection<CategoryProduct> CategoryProduct { get; set; }
    }
}
