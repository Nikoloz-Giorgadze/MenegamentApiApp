using ManagementApi.Domain.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementApi.Persistance.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Lastname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.PersonalNumber)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode();

            builder.Property(x => x.IsActive);
        }
    }
}