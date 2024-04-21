using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Posts.Commands.Delete
{
    public record DeletePostCommand: IRequest<Response>
    {
        public int Id { get; set; }
        public DeletePostCommand(int id)
        {
            Id = id;
        }
    }
}
