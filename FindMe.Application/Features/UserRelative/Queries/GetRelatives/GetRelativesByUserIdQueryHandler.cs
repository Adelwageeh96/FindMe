using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Constants;
using FindMe.Domain.Identity;
using FindMe.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net;
using FindMe.Domain.Models;
using MapsterMapper;
using FindMe.Application.Features.UserRelative.Common;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.UserRelative.Queries.GetRelatives
{
    internal class GetRelativesByUserIdQueryHandler : IRequestHandler<GetRelativesByUserIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetRelativesByUserIdQuery> _stringLocalizer;

        public GetRelativesByUserIdQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<GetRelativesByUserIdQuery> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(GetRelativesByUserIdQuery query, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(query.UserId) is not ApplicationUser user)
            {
                return await Response.FailureAsync("There is no user with this user Id", HttpStatusCode.NotFound);
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.USER))
            {
                return await Response.FailureAsync("Only actors with user role have relatives");
            }

            var relatives = _unitOfWork.Repository<UserRelatives>().Entities().Where(ur => ur.ApplicationUserId == query.UserId);
            var userRelatives=new List<UserRelativeDto>();
            foreach (var relative in relatives)
            {
                userRelatives.Add(_mapper.Map<UserRelativeDto>(relative));
            }

            return await Response.SuccessAsync(userRelatives, _stringLocalizer["Success"].Value);
        }
    }
}
