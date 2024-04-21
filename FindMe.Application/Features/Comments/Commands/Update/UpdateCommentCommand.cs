using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Comments.Commands.Update
{
    public record UpdateCommentCommand: IRequest<Response>
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
