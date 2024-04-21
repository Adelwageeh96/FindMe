
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FindMe.Application.Features.Posts.Commands.Update
{
    public record UpdatePostCommand: IRequest<Response>
    {
        public int Id { get; set; }
        public string Descripation { get; set; }
        public string Address { get; set; }
        public IFormFile Photo { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
