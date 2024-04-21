using FindMe.Shared;
using MediatR;
namespace FindMe.Application.Features.Posts.Queries.GetAll
{
    public record GetAllPostsQuery : PaginatedRequest, IRequest<Response>
    {
    }
}
