using FindMe.Application.Features.Profile.Common;
using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.Profile.Commands
{
    public record EditProfileCommand:IRequest<Response>
    {
        public string UserId { get; set; }
        public UpdateProfileDto profileDto { get; set; }
    }
}
