using FluentValidation;


namespace FindMe.Application.Features.Comments.Commands.Add
{
    public class AddCommentCommandValidator: AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(x=>x.Content).NotEmpty();
            RuleFor(x=>x.ActorId).NotEmpty();
            RuleFor(x=>x.PostId).NotEmpty();

        }
    }
}
