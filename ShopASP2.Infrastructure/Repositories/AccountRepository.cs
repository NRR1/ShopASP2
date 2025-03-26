using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopASP2.Domain.Entities;
using ShopASP2.Domain.Interfaces;

namespace ShopASP2.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> um;
        private readonly SignInManager<User> sim;
        private readonly ILogger<AccountRepository> logger;
        public AccountRepository(UserManager<User> um, SignInManager<User> sim, ILogger<AccountRepository> logger)
        {
            this.um = um;
            this.sim = sim;
            this.logger = logger;
        }
        public async Task<User> Login(User user, string password)
        {
            if (user == null)
            {
                logger.LogError("Передан пустой пользователь");
                return null;
            }
            else
            {
                User? loginUser = await um.FindByEmailAsync(user.Email);
                if (loginUser == null)
                {
                    logger.LogWarning($"Пользователь с email {user.Email} не найден");
                    loginUser = await um.FindByNameAsync(user.UserName);
                }
                if (loginUser != null)
                {
                    bool passwordValue = await um.CheckPasswordAsync(loginUser, password);
                    if (!passwordValue)
                    {
                        logger.LogError("Неверный пароль для пользователя");
                        return null;
                    }
                    logger.LogInformation("Авторизация прошла успешно");
                    return loginUser;
                }
                else
                {
                    logger.LogError("Пользователя не существует");
                    return null;
                }
            }
        }

        public async Task<User> Register(User user, string password)
        {
            if(user == null)
            {
                logger.LogError("Передан пустой пользователь");
                return null;
            }
            if(await um.FindByEmailAsync(user.Email) != null)
            {
                logger.LogWarning("Пользователь уже существует");
                return null;
            }
            if(await um.FindByNameAsync(user.UserName) != null)
            {
                logger.LogWarning("Пользователь уже существует");
                return null;
            }
            User regUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Pathronomic = user.Pathronomic
            };
            IdentityResult registerResult = await um.CreateAsync(regUser, password);
            if (!registerResult.Succeeded)
            {
                logger.LogError("Ошибка регистрации пользователя");
                foreach(var error in registerResult.Errors)
                {
                    logger.LogError(error.Description.ToString());
                }
                return null;
            }
            logger.LogInformation("Регистрация прошла успешно");
            IdentityResult roleResult = await um.AddToRoleAsync(regUser, "User");
            if (!roleResult.Succeeded)
            {
                logger.LogError("Не удалось создать пользователя");
            }
            else
            {
                logger.LogInformation("Пользователь успешно создан");
            }
            return regUser;
        }

        public async Task SignInAsync(User user, bool ispersistent)
        {
            if(user == null)
            {
                logger.LogError("Пользователя не существует/передан пустой пользователь");
                return;
            }
            try
            {
                await sim.SignInAsync(user, isPersistent: ispersistent);
                logger.LogInformation("Пользователь успешно авторизован");
            }
            catch(Exception ex)
            {
                logger.LogError($"Ошибка метода SignInAsync в репозитории, {ex.Message.ToString()}");
            }
        }
    }
}
