

namespace FindMe.Application.Features.Authentication.Common
{
    public record AuthenticationDto
    {
        public string Id { get; set; }
        public string Role {  get; set; }
        public string Token { get; set; }
    }
}
