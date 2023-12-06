using ManagementApi.Domain.Users;
using ManagementApi.Services.UserService.Request;

namespace ManagementApi.Services.UserService
{
    public interface IUserService
    {
        Task<int> Register(UserRegisterModel user);
        Task<User> Login(UserLoginModel user);
        Task Delete(int id);
    }
}