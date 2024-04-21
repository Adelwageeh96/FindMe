using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.PinPost.Queries.GetUserPinnedPosts
{
    public record GetUserPinnedPostsCommand : PaginatedRequest, IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
