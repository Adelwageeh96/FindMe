using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.UserDetail.Queries.GetDetailsByUserId
{
    public class GetDetailsByUserIdQuery: IRequest<Response>
    {
        public string UserId { get; set; }

        public GetDetailsByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
