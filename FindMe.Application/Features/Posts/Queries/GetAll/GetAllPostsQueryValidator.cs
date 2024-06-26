using FluentValidation;


namespace FindMe.Application.Features.Posts.Queries.GetAll
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
        }
    }
}
