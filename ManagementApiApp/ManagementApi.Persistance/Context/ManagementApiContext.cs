using ManagementApi.Domain.UserProfiles;
using ManagementApi.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi.Persistance.Context
{
    public class ManagementApiContext : DbContext
    {
        public ManagementApiContext(DbContextOptions<ManagementApiContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ManagementApiContext).Assembly);
        }
    }
}
