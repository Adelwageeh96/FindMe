using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest
{
    public record GetAllUnapprovedJoinRequestQuery : PaginatedRequest, IRequest<Response>
    {
        
    }
}
