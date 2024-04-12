using FindMe.Application.Features.Authentication.Common;
using FindMe.Domain.Identity;
using Mapster;



namespace FindMe.Application.Common.Mapping
{
    public class Authentication : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(string Role, string Token, ApplicationUser User), AuthenticationDto>.NewConfig()
                .Map(dest => dest.Role, src => src.Role)
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest.Id, src => src.User.Id);
        }
    }
}
