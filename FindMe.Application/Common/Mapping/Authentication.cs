using FindMe.Application.Features.Authentication.Commands.AdminRegister;
using FindMe.Application.Features.Authentication.Commands.UserRegister;
using FindMe.Application.Features.Authentication.Common;
using FindMe.Domain.Constants;
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

            TypeAdapterConfig<(string UserName, UserRegisterCommand UserCommand), ApplicationUser>.NewConfig()
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest, src => src.UserCommand)
                .Map(dest => dest.Gendre, src => MapGender(src.UserCommand.Gendre));

            TypeAdapterConfig<(string UserName, AdminRegisterCommand AdminCommand), ApplicationUser>.NewConfig()
               .Map(dest => dest.UserName, src => src.UserName)
               .Map(dest => dest, src => src.AdminCommand)
               .Map(dest => dest.Gendre, src => MapGender(src.AdminCommand.Gendre));


        }
        private Gendre MapGender(string gender)
        {
            return gender.ToLower() switch
            {
                "male" => Gendre.Male,
                "female" => Gendre.Female,
                _ => throw new ArgumentException($"Invalid gender value: {gender}"),
            };
        }
    }
}
