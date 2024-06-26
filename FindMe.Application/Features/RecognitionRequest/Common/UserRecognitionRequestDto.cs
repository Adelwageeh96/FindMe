

namespace FindMe.Application.Features.RecognitionRequest.Common
{
    public class UserRecognitionRequestDto
    {
        public int Id { get; set; }
        public DateTime? SentAt { get; set; }
        public string Descripation { get; set; }
        public byte[] Photo { get; set; }
    }
}
