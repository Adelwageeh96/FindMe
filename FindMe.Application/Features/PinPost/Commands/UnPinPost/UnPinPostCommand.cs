using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.PinPost.Commands.UnPinPost
{
    public record UnPinPostCommand: IRequest<Response>
    {
        public int PostId { get; set; }
        public string ActorId { get; set; }
    }
}
