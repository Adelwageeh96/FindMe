using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Commands.ApproveRecognitionRequest
{
    public record ApproveRecognitionRequestCommand: IRequest<Response>
    {
        public int Id { get; set; }
        public ApproveRecognitionRequestCommand(int id)
        {
            Id=id;
        }
    }
}
