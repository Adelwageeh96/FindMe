using FindMe.Application.Features.UserDetail.Common;
using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.UserDetail.Commands.AddDetails
{
    public record AddDetailsCommand : IRequest<Response>
    {
        public UserDetailsDto UserDetails { get; set; }
        public string UserId { get; set; }
    }
}
