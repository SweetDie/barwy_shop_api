using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Repositories.Classes;
using DAL.Repositories.Interfaces;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;

namespace BarwyShopAPI
{
    public static class SeederDB
    {
        public static async void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManaager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                var categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

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
                    var result = await userManager.CreateAsync(admin, "123456");
                    result = await userManager.AddToRoleAsync(admin, Roles.Admin);

                    string userEmail = "user@gmail.com";
                    var user = new UserEntity
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FirstName = "User",
                        LastName = "User"
                    };

                    result = await userManager.CreateAsync(user, "123456");
                    result = await userManager.AddToRoleAsync(user, Roles.User);
                }


                // Add products and categories

                if (!categoryRepository.Categories.Any() && !productRepository.Products.Any())
                {

                    await categoryRepository.CreateCategoryAsync(new Category
                    {
                        Name = "Патріотичні"
                    });

                    await categoryRepository.CreateCategoryAsync(new Category
                    {
                        Name = "Пейзажі"
                    });

                    var product = new Product
                    {
                        Name = "Тризуб",
                        DateCreated = DateTime.Now.ToUniversalTime(),
                        Price = 245,
                        Size = "40x50",
                        Article = "0070P1"
                    };
                    await productRepository.CreateAsync(product);
                    await productRepository.AddToCategoryAsync(product, "Патріотичні");

                    product = new Product
                    {
                        Name = "Все буде Україна",
                        DateCreated = DateTime.Now.ToUniversalTime(),
                        Price = 245,
                        Size = "40x50",
                        Article = "0049T1"
                    };
                    await productRepository.CreateAsync(product);
                    //await productRepository.AddToCategoryAsync(product, "Патріотичні");
                }
            }
        }
    }
}