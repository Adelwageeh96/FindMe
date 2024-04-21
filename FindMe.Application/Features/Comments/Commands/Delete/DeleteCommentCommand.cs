using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Comments.Commands.Delete
{
    public record DeleteCommentCommand: IRequest<Response>
    {
        public int Id { get; set; }

        public DeleteCommentCommand(int id)
        {
            Id=id;
        }
    }
}
