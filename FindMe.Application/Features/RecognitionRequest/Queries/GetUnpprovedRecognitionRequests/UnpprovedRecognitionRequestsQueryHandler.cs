using FindMe.Application.Extention;
using FindMe.Application.Features.RecognitionRequest.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using FluentValidation;
using Mapster;
using MediatR;


namespace FindMe.Application.Features.RecognitionRequest.Queries.GetUnpprovedRecognitionRequests
{
    internal class UnpprovedRecognitionRequestsQueryHandler : IRequestHandler<UnpprovedRecognitionRequestsQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UnpprovedRecognitionRequestsQuery> _validator;

        public UnpprovedRecognitionRequestsQueryHandler(
            IUnitOfWork unitOfWork,
            IValidator<UnpprovedRecognitionRequestsQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Response> Handle(UnpprovedRecognitionRequestsQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }

            var entities = _unitOfWork.Repository<RecognitionRequests>()
                                      .Entities()
                                      .Where(rr => !rr.IsApproved)
                                      .OrderByDescending(rr => rr.RecivedAt);

            return await entities.ProjectToType<RecognitionRequestDto>()
                                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        }
    }
}
