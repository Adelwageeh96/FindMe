

namespace FindMe.Domain.Models
{
    public class RecognitionRequestResult : BaseEntity
    {
        public string FirstSimilarityId { get; set; }
        public float FirstSimilarityPercent { get; set; }
        public string? SecondSimilarityId { get; set; }
        public float SecondSimilarityPercent { get; set; }
        public string? ThirdSimilarityId { get; set; }
        public float ThirdSimilarityPercent { get; set; }
        public int RecognitionRequestId { get; set; }
        public virtual RecognitionRequests RecognitionRequest { get; set; }
    }
}
