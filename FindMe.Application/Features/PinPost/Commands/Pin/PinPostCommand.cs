using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.PinPost.Commands.Pin
{
    public record PinPostCommand: IRequest<Response>
    {
        public int PostId { get; set; }
        public string ActorId { get; set; }
    }
}
