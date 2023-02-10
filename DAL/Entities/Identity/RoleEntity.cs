using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.Identity
{
    public sealed class RoleEntity : IdentityRole
    {
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
