using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopASP2.Domain.Entities;

namespace ShopASP2.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            ILogger logger = serviceProvider.GetRequiredService<ILogger<DbInitializer>>();

            string[] roles = { "Admin", "User" };
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation("Роли успешно созданы");
                }
                else
                {
                    logger.LogInformation("Ошибка");
                }
            }
            string adminEmail = "Admin@mail.ru";
            string adminPassword = "@dminPassword667";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser == null)
            {
                adminUser = new User()
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Pathronomic = "Admin",
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    logger.LogInformation("Пользователь успешно создан");
                    var addToRoleAsync = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (addToRoleAsync.Succeeded)
                    {
                        logger.LogInformation($"Пользователю {adminUser.FirstName.ToString()} назначена роль Admin");
                    }
                    else
                    {
                        logger.LogError("Ошибка метода AddToRoleAsync");
                    }
                }
                else
                {
                    logger.LogError("Ошибка создания пользователя(метод CreateAsync)");
                }
            }
            else
            {
                logger.LogError("Пользователь уже существует");
            }
        }
    }
}
