using FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest;
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
                  .Map(dest => dest.ApplicationUser, src => src.Request.ActorId)
                  .Map(dest => dest.Descripation, src => src.Request.Descripation);
                
                


           
        }
    }
}
