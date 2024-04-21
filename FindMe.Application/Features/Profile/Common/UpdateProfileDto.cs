using Microsoft.AspNetCore.Http;


namespace FindMe.Application.Features.Profile.Common
{
    public class UpdateProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public IFormFile Photo { get; set; }
    }
}
