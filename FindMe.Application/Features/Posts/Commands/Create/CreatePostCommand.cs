using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace FindMe.Application.Features.Posts.Commands.Create
{
    public record CreatePostCommand: IRequest<Response>
    {
        public string Descripation {  get;  set; }
        public string Address { get;  set; }
        public IFormFile Photo { get;  set; }
        public string? PhoneNumber { get;  set; }
        public string ActorId { get; set; }
    }
}
