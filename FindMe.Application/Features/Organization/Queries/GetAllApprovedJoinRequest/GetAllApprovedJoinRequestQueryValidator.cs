using FluentValidation;

namespace FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest
{
    public class GetAllApprovedJoinRequestQueryValidator : AbstractValidator<GetAllApprovedJoinRequestQuery>
    {
        public GetAllApprovedJoinRequestQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
}
