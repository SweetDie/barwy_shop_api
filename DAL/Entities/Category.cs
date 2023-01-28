using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public Category()
        {
            Products = new List<Product>();
        }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}
