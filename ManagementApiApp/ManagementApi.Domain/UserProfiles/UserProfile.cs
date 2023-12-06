using ManagementApi.Domain.Users;

namespace ManagementApi.Domain.UserProfiles
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PersonalNumber { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}