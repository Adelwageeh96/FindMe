

using Microsoft.AspNetCore.Http;

namespace FindMe.Application.Features.UserDetail.Common
{
    public class UserDetailsDto
    {
        public string NationalId { get; set; }
        public string MatiralStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Job { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
    }
}
