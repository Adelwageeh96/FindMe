using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Comments.Commands.Add
{
    public record AddCommentCommand: IRequest<Response>
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public string ActorId { get; set; }

    }
}
