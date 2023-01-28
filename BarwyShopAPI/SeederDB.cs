using DAL.Entities;
using DAL.Entities.Identity;
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


                // Add products and categories

                if (!categoryRepository.Categories.Any() && !productRepository.Products.Any())
                {

                    Category[] categories =
                    {
                        new Category
                        {
                            DateCreated = DateTime.Now.ToUniversalTime(),
                            Name = "Патріотичні"
                        },
                        new Category
                        {
                            DateCreated = DateTime.Now.ToUniversalTime(),
                            Name = "Пейзажі"
                        }
                    };

                    Product[] products =
                    {
                        new Product
                        {
                            Name = "Тризуб",
                            DateCreated= DateTime.Now.ToUniversalTime(),
                            Price = 245,
                            Size = "40x50",
                            Article = "0070П1",
                            Categories = new List<Category>()
                            {
                                categories[0]
                            }
                        },
                        new Product
                        {
                            Name = "Все буде Україна",
                            DateCreated= DateTime.Now.ToUniversalTime(),
                            Price = 245,
                            Size = "40x50",
                            Article = "0049Т1",
                            Categories = new List<Category>()
                            {
                                categories[0]
                            }
                        }
                    };

                    categories[0].Products = products;

                    foreach (var item in categories)
                    {
                        await categoryRepository.CreateAsync(item);
                    }
                }
            }
        }
    }
}