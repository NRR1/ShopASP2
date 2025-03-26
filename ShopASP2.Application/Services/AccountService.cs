using AutoMapper;
using Microsoft.Extensions.Logging;
using ShopASP2.Application.DTO;
using ShopASP2.Application.Interfaces;
using ShopASP2.Domain.Entities;
using ShopASP2.Domain.Interfaces;

namespace ShopASP2.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository repository;
        public AccountService(IMapper mapper, ILogger<AccountService> logger, IAccountRepository repository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.repository = repository;
        }

        public Task Login(UserDTO user, string password)
        {
            User loginuser = mapper.Map<User>(user);
            try
            {
                logger.LogInformation("Попытка авторизации в сервисе(маппер)");
                return repository.Login(loginuser, password);
            }
            catch(Exception ex)
            {
                logger.LogInformation("Ошибка авторизации в сервисе");
                logger.LogError(ex.Message.ToString());
                return null;
            }
        }

        public async Task Register(UserDTO user, string password)
        {
            User newUser = mapper.Map<User>(user);
            try
            {
                logger.LogInformation("Попытка регистрации в серсиве(маппер)");
                await repository.Register(newUser, password);
            }
            catch(Exception ex)
            {
                logger.LogInformation("Ошибка регистрации в сервисе");
                logger.LogError(ex.Message.ToString());
            }
        }

        public async Task SignInAsync(UserDTO user, bool ispersistent)
        {
            User simUser = mapper.Map<User>(user);
            try
            {
                logger.LogInformation("Попытка авторизации в методе SignInAsync(маппер)");
                await repository.SignInAsync(simUser, ispersistent);
            }
            catch(Exception ex)
            {
                logger.LogInformation("Ошибка метода SignInAsync(маппер)");
                logger.LogError(ex.Message.ToString());
            }
        }
    }
}
