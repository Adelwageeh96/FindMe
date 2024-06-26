using FindMe.Application.Common.Helpers;
using FindMe.Application.Features.RecognitionResult.Common;
using FindMe.Domain.Models;
using Mapster;

namespace FindMe.Application.Common.Mapping
{
    public class RecognitionRequestResult : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<UserDetails, RecognitionRequestResultDto>.NewConfig()
                .Map(dest => dest.MatiralStatus, src => src.MatiralStatus.ToString())
                .Map(dest => dest.Photo, src => src.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg"))
                .Map(dest => dest.PhoneNumber1, src => src.ApplicationUser.PhoneNumber)
                .Map(dest => dest.PhoneNumber2, src => src.PhoneNumber)
                .Map(dest => dest.Address, src => src.ApplicationUser.Address);

                
        }
    }
}
