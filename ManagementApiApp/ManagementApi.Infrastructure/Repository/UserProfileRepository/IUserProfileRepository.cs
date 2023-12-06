using ManagementApi.Domain.UserProfiles;

namespace ManagementApi.Infrastructure.Repository.UserProfileRepository
{
    public interface IUserProfileRepository
    {
        Task<int> Create(UserProfile profile);
        Task<List<UserProfile>> GetAll();
        Task<UserProfile> Get(int id);
        UserProfile Update(UserProfile profile);
        Task SaveChanges();
    }
}
