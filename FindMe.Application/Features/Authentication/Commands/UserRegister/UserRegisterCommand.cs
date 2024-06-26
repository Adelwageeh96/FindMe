using FindMe.Domain.Constants;
using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Authentication.Commands.UserRegister
{
    public record UserRegisterCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {  get; set; }
        public string Password { get; set; }
        public string Gendre { get; set; }
    }
}
