

using FindMe.Application.Features.Posts.Common;

namespace FindMe.Application.Features.RecognitionRequest.Common
{
    public class RecognitionRequestDto
    {
        public int Id { get; set; }
        public DateTime? SentAt { get; set; }
        public string Descripation { get; set; }
        public byte[] Photo { get; set; }
        public ActorInfromationDto ActorInfromation { get; set; }
    }

}
