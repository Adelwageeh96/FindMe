using FindMe.Domain.Identity;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net;
using FindMe.Application.Common.Helpers;
using FindMe.Application.Features.Profile.Common;
using MapsterMapper;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.Profile.Queries
{
   
    internal class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetProfileQuery> _stringLocalizer;

        public GetProfileQueryHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<GetProfileQuery> stringLocalizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(GetProfileQuery query, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(query.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this Id", HttpStatusCode.NotFound);
            }
            ProfileDto profileDto;
            if(user.Photo is null)
            {
                byte[] defaultPhotoBytes = ReadPhoto.ReadEmbeddedPhoto("DefaultPhoto.jpg");
                profileDto= _mapper.Map<ProfileDto>((defaultPhotoBytes,user));
            }
            else
            {
                profileDto = _mapper.Map<ProfileDto>(user);

            }
            return await Response.SuccessAsync(profileDto, _stringLocalizer["Success"].Value);

        }
    }
}
