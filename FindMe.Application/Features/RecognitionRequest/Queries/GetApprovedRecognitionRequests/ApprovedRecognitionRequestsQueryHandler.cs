using FindMe.Application.Extention;
using FindMe.Application.Features.RecognitionRequest.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetApprovedRecognitionRequests
{
    internal class ApprovedRecognitionRequestsQueryHandler : IRequestHandler<ApprovedRecognitionRequestsQuery, Response>
    {
        private readonly IValidator<ApprovedRecognitionRequestsQuery> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public ApprovedRecognitionRequestsQueryHandler(
            IValidator<ApprovedRecognitionRequestsQuery> validator,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(ApprovedRecognitionRequestsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entities = _unitOfWork.Repository<RecognitionRequests>()
                                      .Entities()
                                      .Where(rr => rr.IsApproved)
                                      .OrderByDescending(rr => rr.RecivedAt);
            return await entities.ProjectToType<RecognitionRequestDto>()
                                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

        }
    }
}
