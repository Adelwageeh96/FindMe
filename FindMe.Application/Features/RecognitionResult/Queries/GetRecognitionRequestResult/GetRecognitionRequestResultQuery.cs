using FindMe.Shared;
using MediatR;


namespace FindMe.Application.Features.RecognitionResult.Queries.GetRecognitionRequestResult
{
    public record GetRecognitionRequestResultQuery: IRequest<Response>
    {
        public int Id { get; set; }
        public GetRecognitionRequestResultQuery(int id)
        {
            Id = id;
        }
    }
}
