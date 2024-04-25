using FindMe.Application.Features.UserDetail.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Net;


namespace FindMe.Application.Features.UserDetail.Queries.GetDetailsByUserId
{
    internal class GetDetailsByUserIdQueryHandler : IRequestHandler<GetDetailsByUserIdQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetDetailsByUserIdQuery> _stringLocalizer;

        public GetDetailsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<GetDetailsByUserIdQuery> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(GetDetailsByUserIdQuery query, CancellationToken cancellationToken)
        {
            if(await _unitOfWork.Repository<UserDetails>().GetItemOnAsync(ud=>ud.ApplicationUserId==query.UserId) is not UserDetails userDetails)
            {
                return await Response.FailureAsync(_stringLocalizer["UserNotFound"].Value,HttpStatusCode.NotFound);
            }

            var getUserDetailsDto = _mapper.Map<GetUserDetailsDto>(userDetails);
            return await Response.SuccessAsync(getUserDetailsDto);
            
        }
    }
}
