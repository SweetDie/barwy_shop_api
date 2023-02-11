using DAL.Entities.Identity;

namespace DAL.Entities.Image ;

    public class UserImageEntity : BaseFileEntity
    {
        public virtual UserEntity User { get; set; }
    }