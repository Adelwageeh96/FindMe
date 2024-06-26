using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;


namespace FindMe.Application.Features.RecognitionRequest.Commands.DisapproveRecognitionRequest
{
    internal class DisapproveRecognitionRequestCommandHandler : IRequestHandler<DisapproveRecognitionRequestCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DisapproveRecognitionRequestCommand> _stringLocalizer;

        public DisapproveRecognitionRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<DisapproveRecognitionRequestCommand> stringLocalizer)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response> Handle(DisapproveRecognitionRequestCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<RecognitionRequests>().GetByIdAsync(command.Id) is not RecognitionRequests recognitionRequest)
            {
                return await Response.FailureAsync("There is no request with this Id");
            }

            if (recognitionRequest.IsApproved)
            {
                return await Response.FailureAsync("Request already approved");
            }

            await _unitOfWork.Repository<RecognitionRequests>().DeleteAsync(recognitionRequest);
            await _unitOfWork.SaveAsync();

            return await Response.SuccessAsync(_stringLocalizer["RequestDisapproval"].Value);
        }
    }
}
