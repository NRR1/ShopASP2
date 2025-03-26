using ShopASP2.Domain.Entities;

namespace ShopASP2.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(User user, string password);
        Task SignInAsync(User user, bool ispersistent);
    }
}
