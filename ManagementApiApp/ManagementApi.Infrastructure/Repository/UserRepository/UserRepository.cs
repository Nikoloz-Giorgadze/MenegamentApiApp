using ManagementApi.Domain.Users;
using ManagementApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Infrastructure.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ManagementApiContext _context;

        public UserRepository(ManagementApiContext context)
        {
            _context = context;
        }
        public async Task<int> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        public async Task<bool> Exist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User> Get(string email, string password)
        {
            var entity = await _context.Users.Where(x => x.IsActive == true)
                .SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

            if (entity is null)
                throw new Exception("Email or password doesn't exist");
            return entity;
        }

        public async Task<User> Get(int id)
        {
            var entity = await _context.Users.Where(x => x.IsActive == true)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (entity is null)
                throw new Exception("this user doesn't exist");
          
            return entity;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
