using ManagementApi.Domain.UserProfiles;
using ManagementApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Infrastructure.Repository.UserProfileRepository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ManagementApiContext _context;
        public UserProfileRepository(ManagementApiContext context)
        {
            _context = context;
        }

        public async Task<int> Create(UserProfile profile)
        {
            await _context.AddAsync(profile);
            return profile.Id;
        }

        public async Task<List<UserProfile>> GetAll()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task<UserProfile> Get(int id)
        {
            var entity = await _context.UserProfiles.Where(x => x.IsActive == true).FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                throw new Exception("User profile doesn't exist");

            return entity;
        }

        public UserProfile Update(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            return profile;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}