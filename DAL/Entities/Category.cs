using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : BaseEntity<Guid>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public string NormalizedName { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProduct { get; set; }
    }
}
