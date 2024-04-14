using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Authentication.Commands.LogOut
{
    public record LogOutCommand : IRequest<Response>
    {
        public string FcmToken { get; set; }
    }
}
