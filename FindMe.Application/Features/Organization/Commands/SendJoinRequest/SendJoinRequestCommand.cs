using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FindMe.Application.Features.Organization.Commands.SendJoinRequest
{
    public record SendJoinRequestCommand: IRequest<Response>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile  RequestPhoto { get; set; }
    }
}
