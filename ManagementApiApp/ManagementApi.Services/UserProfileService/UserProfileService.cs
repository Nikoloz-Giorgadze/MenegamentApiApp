using ManagementApi.Domain.UserProfiles;
using ManagementApi.Infrastructure.Repository.UserProfileRepository;
using ManagementApi.Services.UserProfileService.Request;
using Mapster;

namespace ManagementApi.Services.UserProfileService
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repo;

        public UserProfileService(IUserProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Create(int ownerId, UserProfileModel model)
        {
            if (model is null)
                throw new ArgumentException("Wrong model");

            if (model.Firstname is null)
                throw new Exception("First name is required!");

            if (model.Lastname is null)
                throw new Exception("Last name is required!");

            if (model.PersonalNumber is null)
                throw new Exception("Personal number is required!");

            var userProfile = model.Adapt<UserProfile>();

            userProfile.UserId = ownerId;
            userProfile.IsActive = true;

            await _repo.Create(userProfile);
            await _repo.SaveChanges();

            return userProfile.Id;
        }

        public async Task<List<UserProfileGetAllProfiles>> GetAll()
        {
            var result = await _repo.GetAll();

            return result.Adapt<List<UserProfileGetAllProfiles>>();
        }

        public async Task<UserProfileResponseModel> Get(int id)
        {
            var result = await _repo.Get(id);

            return result.Adapt<UserProfileResponseModel>();
        }

        public async Task<UserProfileResponseModel> Update(int id, UserProfileModel model)
        {
            var userProfile = await _repo.Get(id);

            if (userProfile is null)
                throw new Exception("User profile doesn't exist");

            userProfile.Firstname = model.Firstname;
            userProfile.Lastname = model.Lastname;
            userProfile.PersonalNumber = model.PersonalNumber;

            _repo.Update(userProfile);

            await _repo.SaveChanges();

            return userProfile.Adapt<UserProfileResponseModel>();
        }

        public async Task<UserProfile> Delete(int id)
        {
            var userProfile = await _repo.Get(id);
            if (userProfile is null)
                throw new Exception("This user profile doesn't exists");

            userProfile.IsActive = false;

            await _repo.SaveChanges();
            return userProfile.Adapt<UserProfile>();
        }
    }
}