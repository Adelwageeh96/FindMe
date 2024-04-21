using FindMe.Application.Features.UserRelative.Common;
using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.UserRelative.Commands.AddRelatives
{
    public record AddRelativesCommand : IRequest<Response>
    {
        public List<UserRelativeDto> UserRelatives { get; set; }
        public string UserId { get; set; }
        
    }
}
