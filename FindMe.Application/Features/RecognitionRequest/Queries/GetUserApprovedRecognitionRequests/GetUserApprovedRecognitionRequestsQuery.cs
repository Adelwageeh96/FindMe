using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUserApprovedRecognitionRequests
{
    public record GetUserApprovedRecognitionRequestsQuery: PaginatedRequest,IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
