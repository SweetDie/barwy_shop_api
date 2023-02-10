using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public sealed class Category : BaseEntity<Guid>
    {
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string NormalizedName { get; set; }

        public ICollection<CategoryProduct> CategoryProduct { get; set; }
    }
}
