using FindMe.Application.Features.UserRelative.Common;
using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.UserRelative.Queries.GetRelatives
{
    public class GetRelativesByUserIdQuery: IRequest<Response>
    {
        public string UserId { get; set; }

        public GetRelativesByUserIdQuery(string userId)
        {
            UserId=userId;
        }

    }
}
