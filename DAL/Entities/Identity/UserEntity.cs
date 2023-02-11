using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using DAL.Entities.Image;

namespace DAL.Entities.Identity
{
    public sealed class UserEntity : IdentityUser
    {
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }

        public Guid ImageId { get; set; }
        public UserImageEntity Image { get; set; }
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
