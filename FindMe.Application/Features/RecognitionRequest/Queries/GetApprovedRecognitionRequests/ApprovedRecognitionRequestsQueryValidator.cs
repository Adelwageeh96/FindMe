using FluentValidation;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetApprovedRecognitionRequests
{
    public class ApprovedRecognitionRequestsQueryValidator: AbstractValidator<ApprovedRecognitionRequestsQuery>
    {
        public ApprovedRecognitionRequestsQueryValidator()
        {
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.KeyWord).Null();
        }
    }
}
