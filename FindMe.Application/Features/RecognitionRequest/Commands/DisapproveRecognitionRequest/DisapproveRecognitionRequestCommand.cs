using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.RecognitionRequest.Commands.DisapproveRecognitionRequest
{
    public record DisapproveRecognitionRequestCommand: IRequest<Response>
    {
        public int Id { get; set; }
        public DisapproveRecognitionRequestCommand(int id)
        {
            Id = id;
        }
    }

}
