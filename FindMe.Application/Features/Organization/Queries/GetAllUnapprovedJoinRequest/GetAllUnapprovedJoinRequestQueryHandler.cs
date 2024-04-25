using FindMe.Application.Extention;
using FindMe.Application.Features.Organization.Queries.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace FindMe.Application.Features.Organization.Queries.GetAllUnapprovedJoinRequest
{
    
    public class GetAllUnapprovedJoinRequestQueryHandler : IRequestHandler<GetAllUnapprovedJoinRequestQuery, Response>
    {
        private readonly IValidator<GetAllUnapprovedJoinRequestQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUnapprovedJoinRequestQueryHandler(
            IValidator<GetAllUnapprovedJoinRequestQuery> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetAllUnapprovedJoinRequestQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entites = _unitOfWork.Repository<OrganizaitonJoinRequest>().Entities();

            entites = entites.Where(e => !e.IsApproved );


            entites = entites.OrderByDescending(x => x.RecivedAt);

            return await entites.ProjectToType<JoinRequestDto>()
                                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
