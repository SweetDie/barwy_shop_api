using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.Identity
{
    public class UserRoleEntity : IdentityUserRole<string>
    {
        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
