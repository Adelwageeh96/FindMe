using FindMe.Application.Features.UserDetail.Common;
using FindMe.Shared;
using MediatR;

namespace FindMe.Application.Features.UserDetail.Commands.UpdateDetails
{
    public record UpdateDetailsCommand: IRequest<Response>
    {
        public string UserId { get; set; }
        public UserDetailsDto UserDetails { get; set; }
        
    }
}
