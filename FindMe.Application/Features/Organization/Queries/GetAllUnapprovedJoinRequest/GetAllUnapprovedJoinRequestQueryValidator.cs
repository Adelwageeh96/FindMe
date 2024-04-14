using FluentValidation;

namespace FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest
{
    public class GetAllUnapprovedJoinRequestQueryValidator : AbstractValidator<GetAllUnapprovedJoinRequestQuery>
    {
        public GetAllUnapprovedJoinRequestQueryValidator()
        {
            RuleFor(x=>x.PageNumber).NotEmpty();
            RuleFor(x=>x.PageSize).NotEmpty();
        }
    }
}
