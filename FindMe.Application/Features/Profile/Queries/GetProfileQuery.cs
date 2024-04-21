using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Profile.Queries
{
    public record GetProfileQuery: IRequest<Response>
    {
        public string UserId { get; set; }
        public GetProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
