using FindMe.Application.Features.RecognitionResult.Common;
using FindMe.Application.Features.UserRelative.Common;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Domain.Identity;
using FindMe.Domain.Models;
using FindMe.Shared;
using Mapster;
using MapsterMapper;
using MediatR;

namespace FindMe.Application.Features.RecognitionResult.Queries.GetRecognitionRequestResult
{
    internal class GetRecognitionRequestResultQueryHandler : IRequestHandler<GetRecognitionRequestResultQuery, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRecognitionRequestResultQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(GetRecognitionRequestResultQuery query, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<RecognitionRequestResult>().GetItemOnAsync(r=>r.RecognitionRequestId == query.Id);
            if (result == null)
            {
                return await Response.FailureAsync("There is no recognition result for this id");
            }
            var recognitionRequestResultDtos = new List<RecognitionRequestResultDto>();
            var similarityIds = new List<string?> { result.FirstSimilarityId, result.SecondSimilarityId, result.ThirdSimilarityId };
            for (int i = 0; i < 3; i++) 
            {
                if (i == 0 && result.FirstSimilarityPercent == 0)
                    continue;
                else if(i==1 && result.SecondSimilarityPercent == 0)
                    continue;
                else if(i==2 && result.ThirdSimilarityPercent == 0)
                    continue;

                var userDetails = await _unitOfWork.Repository<UserDetails>().GetItemOnAsync(ud => ud.ApplicationUserId == similarityIds[i]);
                var userRelatives = _unitOfWork.Repository<UserRelatives>().Entities().Where(ur => ur.ApplicationUserId == similarityIds[i]).ToList();
                if (userDetails is null)
                    continue;

                var dto = _mapper.Map<RecognitionRequestResultDto>(userDetails);
                dto.Name = userDetails.ApplicationUser.Name;

                switch (i)
                {
                    case 0 :
                        dto.SimilarityPercent = result.FirstSimilarityPercent;
                        break;
                    case 1:
                        dto.SimilarityPercent = result.SecondSimilarityPercent;
                        break;
                    case 2:
                        dto.SimilarityPercent = result.ThirdSimilarityPercent;
                        break;

                }
                dto.Relatives = userRelatives.Adapt<List<UserRelativeDto>>();
                recognitionRequestResultDtos.Add(dto);
            }

            return await Response.SuccessAsync(recognitionRequestResultDtos);
        }

    }
}
