using DAL.Entities;
using DAL.Entities.Identity;
using DAL.Repositories.Interfaces;
using Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;

namespace BarwyShopAPI
{
    public static class SeederDb
    {
        public static async void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
                var categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                if (!roleManager.Roles.Any())
                {
                   await roleManager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.Admin
                    });

                    await roleManager.CreateAsync(new RoleEntity
                    {
                        Name = Roles.User
                    });
                }

                if (!userManager.Users.Any())
                {
                    const string adminEmail = "admin@gmail.com";    
                    var admin = new UserEntity
                    {                         
                        Email = adminEmail,   
                        UserName = adminEmail,
                        FirstName = "Admin",  
                        LastName = "Admin"    
                    };
                    await userManager.CreateAsync(admin, "123456");
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                    
                    const string userEmail = "user@gmail.com";
                    var user = new UserEntity
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FirstName = "User",
                        LastName = "User"
                    };
                    await userManager.CreateAsync(user, "123456");
                    await userManager.AddToRoleAsync(user, Roles.User);
                }

                if (!categoryRepository.Categories.Any())
                {
                    var category = new Category
                    {
                        Name = "Патріотичні",
                        NormalizedName = "Патріотичні".ToUpper(),
                        DateCreated = DateTime.Now.ToUniversalTime()
                    };
                    await categoryRepository.CreateAsync(category);

                    category = new Category
                    {
                        Name = "Пейзажі",
                        NormalizedName = "Пейзажі".ToUpper(),
                        DateCreated = DateTime.Now.ToUniversalTime()
                    };
                    await categoryRepository.CreateAsync(category);

                    category = new Category
                    {
                        Name = "Uncategorized",
                        NormalizedName = "Uncategorized".ToUpper(),
                        DateCreated = DateTime.Now.ToUniversalTime()
                    };
                    await categoryRepository.CreateAsync(category);
                }

                if (!productRepository.Products.Any())
                {
                    var product = new Product
                    {
                        Name = "Все буде Україна",
                        DateCreated = DateTime.Now.ToUniversalTime(),
                        Price = 245,
                        Size = "40x50",
                        Article = "0049Т1",
                        Image = "Product/5jghk0xy.iff.jpg"
                    };
                    await productRepository.CreateAsync(product);
                    await productRepository.AddToCategoryAsync(product, "Патріотичні");
                    
                    product = new Product
                    {
                        Name = "Тризуб",
                        DateCreated = DateTime.Now.ToUniversalTime(),
                        Price = 245,
                        Size = "40x50",
                        Article = "0070П1",
                        Image = "Product/j2iosauv.nv4.jpg"
                    };
                    await productRepository.CreateAsync(product);
                    await productRepository.AddToCategoryAsync(product, "Патріотичні");
                }
            }
        }
    }
}