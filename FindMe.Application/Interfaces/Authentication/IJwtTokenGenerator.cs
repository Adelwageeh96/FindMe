using FindMe.Domain.Identity;


namespace FindMe.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser user, string role);
    }
}
