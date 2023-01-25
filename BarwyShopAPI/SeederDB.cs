using BarwyShopAPI.Constants;
using DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BarwyShopAPI
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManaager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                if (!roleManaager.Roles.Any())
                {
                    var result = roleManaager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.Admin
                    }).Result;
                    result = roleManaager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.User
                    }).Result;
                }

                if (!userManager.Users.Any())
                {
                    string adminEmail = "admin@gmail.com";
                    var admin = new UserEntity
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = "Admin",
                        LastName = "Admin"
                    };
                    var result = userManager.CreateAsync(admin, "123456").Result;
                    result = userManager.AddToRoleAsync(admin, Roles.Admin).Result;

                    string userEmail = "user@gmail.com";
                    var user = new UserEntity
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FirstName = "User",
                        LastName = "User"
                    };

                    result = userManager.CreateAsync(user, "123456").Result;
                    result = userManager.AddToRoleAsync(user, Roles.User).Result;
                }
            }
        }
    }
}