using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Posts.Queries.GetById
{
    public record GetPostByIdQuery: IRequest<Response>
    {
        public int Id { get; set; }
        public GetPostByIdQuery(int id)
        {
            Id= id;
        }
    }
}
