using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest
{
    public record GetAllApprovedJoinRequestQuery : PaginatedRequest, IRequest<Response>
    {
    }
}
