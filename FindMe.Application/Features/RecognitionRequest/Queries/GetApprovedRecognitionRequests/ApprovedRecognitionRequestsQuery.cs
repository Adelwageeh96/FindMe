using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetApprovedRecognitionRequests
{
    public record ApprovedRecognitionRequestsQuery : PaginatedRequest, IRequest<Response>
    {

    }
}
