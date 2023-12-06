using ManagementApi.Domain.Users;

namespace ManagementApi.Services.UserProfileService.Request
{
    public class UserProfileResponseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PersonalNumber { get; set; }
    }
}