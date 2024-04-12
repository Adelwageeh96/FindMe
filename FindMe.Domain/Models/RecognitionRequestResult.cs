

namespace FindMe.Domain.Models
{
    public class RecognitionRequestResult : BaseEntity
    {
        public string FirstSimilarityId { get; set; }
        public string? SecondSimilarityId { get; set; }
        public string? ThirdSimilarityId { get; set; }
        public int RecognitionRequestId { get; set; }
        public virtual RecognitionRequest RecognitionRequest { get; set; }
    }
}
