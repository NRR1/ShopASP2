using ShopASP2.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopASP2.Application.Interfaces
{
    public interface IAccountService
    {
        Task Register(UserDTO user, string password);
        Task Login(UserDTO user, string password);
        Task SignInAsync(UserDTO user, bool ispersistent);
    }
}
