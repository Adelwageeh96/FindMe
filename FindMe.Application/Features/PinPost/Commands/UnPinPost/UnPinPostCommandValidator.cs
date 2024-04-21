using FluentValidation;


namespace FindMe.Application.Features.PinPost.Commands.UnPinPost
{
    public class UnPinPostCommandValidator : AbstractValidator<UnPinPostCommand>
    {
        public UnPinPostCommandValidator()
        {
            RuleFor(x => x.ActorId).NotEmpty();
            RuleFor(x => x.PostId).NotEmpty();
        }
    }
}
