using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.Authentication.Commands.AdminRegister
{
    public record AdminRegisterCommand : IRequest<Response>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gendre { get; set; }
    }
}
