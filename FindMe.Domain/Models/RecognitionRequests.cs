using FindMe.Domain.Identity;


namespace FindMe.Domain.Models
{
    public class RecognitionRequests : BaseEntity
    {
        public string Descripation { get; set; }
        public DateTime RecivedAt { get; set; } 
        public bool IsApproved { get; set; }
        public byte[] Photo { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual RecognitionRequestResult RecognitionRequestResult { get; set; }

        public RecognitionRequests()
        {
            RecivedAt = DateTime.UtcNow;
        }
    }
}
