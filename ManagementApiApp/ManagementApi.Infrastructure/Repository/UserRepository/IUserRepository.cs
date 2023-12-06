using ManagementApi.Domain.Users;

namespace ManagementApi.Infrastructure.Repository.UserRepository
{
    public interface IUserRepository
    {
        Task<int> Create(User user);
        Task<bool> Exist(string email);
        Task<User> Get(string email, string password);
        Task<User> Get(int id);
        Task SaveChanges();
    }
}
