using FluentValidation;

namespace FindMe.Application.Features.RecognitionRequest.Commands.SendRecognitionRequest
{
    internal class SendRecognitionRequestCommandValidator : AbstractValidator<SendRecognitionRequestCommand>
    {
        public SendRecognitionRequestCommandValidator()
        {
            RuleFor(x=>x.ActorId).NotEmpty();
            RuleFor(x=>x.Photo).NotEmpty();
            RuleFor(x=>x.Descripation).NotEmpty();
        }
    }
}
