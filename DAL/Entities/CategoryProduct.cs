using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CategoryProduct
    {

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
