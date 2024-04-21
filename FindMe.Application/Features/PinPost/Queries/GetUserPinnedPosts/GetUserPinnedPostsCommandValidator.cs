

using FluentValidation;

namespace FindMe.Application.Features.PinPost.Queries.GetUserPinnedPosts
{
    internal class GetUserPinnedPostsCommandValidator: AbstractValidator<GetUserPinnedPostsCommand>
    {
        public GetUserPinnedPostsCommandValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).NotEmpty();
            RuleFor(x=>x.UserId).NotEmpty();
        }
    }
}
