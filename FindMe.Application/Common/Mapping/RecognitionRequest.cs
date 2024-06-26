using FindMe.Application.Common.Helpers;
using FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest;
using FindMe.Application.Features.RecognitionRequest.Common;
using FindMe.Domain.Models;
using Mapster;


namespace FindMe.Application.Common.Mapping
{
    public class RecognitionRequest : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<(SendRecognitionRequestCommand Request, byte[] Photo), RecognitionRequests>.NewConfig()
                  .Map(dest => dest.Photo, src => src.Photo)
                  .Map(dest => dest.ApplicationUserId, src => src.Request.ActorId)
                  .Map(dest => dest.Descripation, src => src.Request.Descripation);


            TypeAdapterConfig<RecognitionRequests, RecognitionRequestDto>.NewConfig()
                .Map(dest => dest.ActorInfromation.Name, src => src.ApplicationUser.Name)
                .Map(dest => dest.ActorInfromation.Photo, src => src.ApplicationUser.Photo ?? ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg"))
                .Map(dest => dest.ActorInfromation.Id, src => src.ApplicationUser.Id)
                .Map(dest => dest.SentAt, src => src.RecivedAt);


            TypeAdapterConfig<RecognitionRequests, UserRecognitionRequestDto>.NewConfig()
                .Map(dest => dest.SentAt, src => src.RecivedAt);
                


           
        }
    }
}
