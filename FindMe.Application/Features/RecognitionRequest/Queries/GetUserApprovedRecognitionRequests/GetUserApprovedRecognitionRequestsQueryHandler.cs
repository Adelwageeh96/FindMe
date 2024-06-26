using FindMe.Application.Extention;
using FindMe.Application.Features.RecognitionRequest.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using System.Globalization;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUserApprovedRecognitionRequests
{
    internal class GetUserApprovedRecognitionRequestsQueryHandler : IRequestHandler<GetUserApprovedRecognitionRequestsQuery, Response>
    {
        private readonly IValidator<GetUserApprovedRecognitionRequestsQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserApprovedRecognitionRequestsQueryHandler(
            IValidator<GetUserApprovedRecognitionRequestsQuery> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(GetUserApprovedRecognitionRequestsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entities = _unitOfWork.Repository<RecognitionRequests>()
                                      .Entities()
                                      .Where(rr => rr.ApplicationUserId==query.UserId && rr.IsApproved)
                                      .OrderByDescending(rr => rr.RecivedAt);

            return await entities.ProjectToType<UserRecognitionRequestDto>()
                                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
