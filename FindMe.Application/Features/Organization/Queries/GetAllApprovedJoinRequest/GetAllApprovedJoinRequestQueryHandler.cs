using FindMe.Application.Extention;
using FindMe.Application.Features.Organization.Queries.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;


namespace FindMe.Application.Features.Organization.Queries.GetAllApprovedJoinRequest
{
    
    public class GetAllApprovedJoinRequestQueryHandler : IRequestHandler<GetAllApprovedJoinRequestQuery, Response>
    {
        private readonly IValidator<GetAllApprovedJoinRequestQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllApprovedJoinRequestQueryHandler(
            IValidator<GetAllApprovedJoinRequestQuery> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response> Handle(GetAllApprovedJoinRequestQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entites = _unitOfWork.Repository<OrganizaitonJoinRequest>().Entities();
            entites = entites.Where(e => e.IsApproved);

            if (!query.KeyWord.IsNullOrEmpty())
            {
                entites = entites.Where(x => x.Name.ToLower().Contains(query.KeyWord.ToLower()));
            }
            entites = entites.OrderByDescending(x => x.RecivedAt);
            return await entites.ProjectToType<JoinRequestDto>()
                                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
