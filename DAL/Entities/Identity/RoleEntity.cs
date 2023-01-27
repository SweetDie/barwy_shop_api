using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.Identity
{
    public class RoleEntity : IdentityRole
    {
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
