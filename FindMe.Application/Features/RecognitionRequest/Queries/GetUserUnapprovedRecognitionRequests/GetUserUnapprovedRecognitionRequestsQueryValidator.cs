using FluentValidation;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUserUnapprovedRecognitionRequests
{
    public class GetUserUnapprovedRecognitionRequestsQueryValidator: AbstractValidator<GetUserUnapprovedRecognitionRequestsQuery>
    {
        public GetUserUnapprovedRecognitionRequestsQueryValidator()
        {
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x => x.KeyWord).Null();
        }
    }
}
