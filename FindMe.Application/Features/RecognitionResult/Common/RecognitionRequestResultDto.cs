using FindMe.Application.Features.UserRelative.Common;
using FindMe.Domain.Constants;

namespace FindMe.Application.Features.RecognitionResult.Common
{
    public class RecognitionRequestResultDto
    {
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string MatiralStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Job { get; set; }
        public byte[] Photo { get; set; }
        public string PhoneNumber1 { get; set; }
        public string Address {  get; set; }
        public float SimilarityPercent { get; set; }
        public string? PhoneNumber2 { get; set; }
        public string? Notes { get; set; }
        public List<UserRelativeDto> Relatives { get; set; }
    }
}
