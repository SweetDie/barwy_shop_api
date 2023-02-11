namespace DAL.Entities.Image ;

    public class ProductImageEntity : BaseFileEntity
    {
        public virtual Product Product { get; set; }
    }