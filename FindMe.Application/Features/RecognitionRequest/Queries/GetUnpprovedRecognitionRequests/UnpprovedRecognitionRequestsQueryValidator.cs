using FluentValidation;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUnpprovedRecognitionRequests
{
    public class UnpprovedRecognitionRequestsQueryValidator: AbstractValidator<UnpprovedRecognitionRequestsQuery>
    {
        public UnpprovedRecognitionRequestsQueryValidator()
        {
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.KeyWord).Null();
        }
    }
}
