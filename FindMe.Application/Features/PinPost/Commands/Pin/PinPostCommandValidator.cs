using FluentValidation;


namespace FindMe.Application.Features.PinPost.Commands.Pin
{
    public class PinPostCommandValidator: AbstractValidator<PinPostCommand>
    {
        public PinPostCommandValidator()
        {
            RuleFor(x=>x.ActorId).NotEmpty();
            RuleFor(x=>x.PostId).NotEmpty();
        }
    }
}
