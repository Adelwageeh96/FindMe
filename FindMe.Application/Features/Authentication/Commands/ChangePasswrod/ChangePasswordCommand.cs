using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Authentication.Commands.ChangePasswrod
{
    public record ChangePasswordCommand:IRequest<Response>
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
