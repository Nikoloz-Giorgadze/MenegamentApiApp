using ManagementApi.Domain.UserProfiles;
using ManagementApi.Services.UserProfileService.Request;

namespace ManagementApi.Services.UserProfileService
{
    public interface IUserProfileService
    {
        Task<int> Create(int ownerId,UserProfileModel model);
        Task<List<UserProfileGetAllProfiles>> GetAll();
        Task<UserProfileResponseModel> Get(int id);
        Task<UserProfileResponseModel> Update(int id, UserProfileModel profile);
        Task<UserProfile> Delete(int id);
    }
}
