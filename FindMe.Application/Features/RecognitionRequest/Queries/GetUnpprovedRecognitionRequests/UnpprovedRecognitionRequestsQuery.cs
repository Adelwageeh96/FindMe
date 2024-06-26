using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUnpprovedRecognitionRequests
{
    public record UnpprovedRecognitionRequestsQuery : PaginatedRequest, IRequest<Response>
    {
    }
}
