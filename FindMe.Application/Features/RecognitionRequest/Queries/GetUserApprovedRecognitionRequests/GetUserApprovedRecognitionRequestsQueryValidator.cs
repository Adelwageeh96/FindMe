using FluentValidation;

namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUserApprovedRecognitionRequests
{
    public class GetUserApprovedRecognitionRequestsQueryValidator : AbstractValidator<GetUserApprovedRecognitionRequestsQuery>
    {
        public GetUserApprovedRecognitionRequestsQueryValidator()
        {
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.KeyWord).Null();
        }
    }
}
