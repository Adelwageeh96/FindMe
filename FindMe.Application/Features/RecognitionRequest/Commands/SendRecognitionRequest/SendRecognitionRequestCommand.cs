using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest
{
    public record SendRecognitionRequestCommand: IRequest<Response>
    {
        public string Descripation {  get; set; }
        public string ActorId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
