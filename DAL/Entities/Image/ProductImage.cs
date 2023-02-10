using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.Image ;

    public class ProductImage : BaseImage
    {
        public virtual Product Product { get; set; }
    }