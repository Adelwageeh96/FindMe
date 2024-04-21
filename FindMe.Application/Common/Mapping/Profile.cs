using FindMe.Application.Common.Mapping.Helpers;
using FindMe.Application.Features.Profile.Commands;
using FindMe.Application.Features.Profile.Common;
using FindMe.Domain.Identity;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    public class Profile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(byte[] Photo, ApplicationUser User), ProfileDto>.NewConfig()
                .Map(dest => dest.Photo, src => src.Photo)
                .Map(dest => dest, src => src.User)
                .Map(dest => dest.Gender, src => src.User.Gendre.ToString());

            TypeAdapterConfig<ApplicationUser, ProfileDto>.NewConfig()
                .Map(dest => dest.Gender, src => src.Gendre.ToString());

            TypeAdapterConfig<UpdateProfileDto, ApplicationUser>.NewConfig()
                .Map(dest => dest.Gendre, src => Map.MapGender(src.Gender))
                .Ignore(dest => dest.Photo);

        }
    }
}
