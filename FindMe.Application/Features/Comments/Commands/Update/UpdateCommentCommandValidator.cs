using FluentValidation;


namespace FindMe.Application.Features.Comments.Commands.Update
{
    internal class UpdateCommentCommandValidator: AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(x=>x.Content).NotEmpty();
            RuleFor(x=>x.Id).NotEmpty();
        }
    }
}
