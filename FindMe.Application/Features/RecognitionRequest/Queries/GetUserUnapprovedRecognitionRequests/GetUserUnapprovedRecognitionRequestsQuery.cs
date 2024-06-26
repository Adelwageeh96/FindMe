using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUserUnapprovedRecognitionRequests
{
    public record GetUserUnapprovedRecognitionRequestsQuery: PaginatedRequest ,IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
