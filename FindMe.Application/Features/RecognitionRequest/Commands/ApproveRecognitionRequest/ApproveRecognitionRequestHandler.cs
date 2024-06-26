using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FindMe.Application.Common.Helpers;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Models;
using FindMe.Shared;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FindMe.Application.Features.RecognitionRequest.Commands.ApproveRecognitionRequest
{
    internal class ApproveRecognitionRequestHandler : IRequestHandler<ApproveRecognitionRequestCommand, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ApproveRecognitionRequestCommand> _stringLocalizer;
        private readonly IFastApiService _fastApiService;

        public ApproveRecognitionRequestHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<ApproveRecognitionRequestCommand> stringLocalizer,
            IFastApiService fastApiService)
        {
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _fastApiService = fastApiService;
        }

        public async Task<Response> Handle(ApproveRecognitionRequestCommand command, CancellationToken cancellationToken)
        {
            var recognitionRequest = await _unitOfWork.Repository<RecognitionRequests>().GetByIdAsync(command.Id);
            if (recognitionRequest == null)
            {
                return await Response.FailureAsync("There is no request with this Id");
            }

            if (recognitionRequest.IsApproved)
            {
                return await Response.FailureAsync("Request already approved");
            }

            var photoBytes = recognitionRequest.Photo;
            var embedding = await _fastApiService.GenerateEmbeddingAsync(photoBytes);

            var userDetailsList = await _unitOfWork.Repository<UserDetails>().GetAllAsync();
            var embeddingsDict = userDetailsList.ToDictionary(
                ud => ud.ApplicationUserId,
                ud => EmbeddingVectorConverter.ToFloatList(ud.EmbeddingVector)
            );

            try
            {
                var similarities = await _fastApiService.CalculateSimilaritiesAsync(embedding, embeddingsDict);

                var recognitionRequestResult = new RecognitionRequestResult
                {
                    RecognitionRequestId = command.Id,
                    FirstSimilarityId = similarities.ElementAtOrDefault(0)?.PhotoId,
                    FirstSimilarityPercent = similarities.ElementAtOrDefault(0)?.Similarity ?? 0,
                    SecondSimilarityId = similarities.ElementAtOrDefault(1)?.PhotoId,
                    SecondSimilarityPercent = similarities.ElementAtOrDefault(1)?.Similarity ?? 0,
                    ThirdSimilarityId = similarities.ElementAtOrDefault(2)?.PhotoId,
                    ThirdSimilarityPercent = similarities.ElementAtOrDefault(2)?.Similarity ?? 0
                };
                recognitionRequest.IsApproved = true;

                await _unitOfWork.Repository<RecognitionRequestResult>().AddAsync(recognitionRequestResult);
                await _unitOfWork.SaveAsync();

                return await Response.SuccessAsync(_stringLocalizer["RequestApproval"].Value);
            }
            catch (Exception ex)
            {
                // Log the error (this can be more sophisticated based on the logging framework you're using)
                return await Response.FailureAsync($"An error occurred while processing the recognition request: {ex.Message}");
            }
        }

    }
}
