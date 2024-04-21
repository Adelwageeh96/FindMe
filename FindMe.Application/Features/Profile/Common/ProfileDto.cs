

namespace FindMe.Application.Features.Profile.Common
{
    public class ProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public byte[]? Photo { get; set; }
    }
}
