using FindMe.Application.Features.UserRelative.Common;
using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.UserRelative.Commands.UpdateRelatives
{
    public record UpdateRelativesCommand: IRequest<Response>
    {
        public List<UserRelativeDto> UserRelatives { get; set; }
    }
}
