using FindMe.Domain.Identity;


namespace FindMe.Domain.Models
{
    public class PinnedPost : BaseEntity
    {
        public DateTime PinnedAt { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public PinnedPost()
        {
            PinnedAt = DateTime.UtcNow;
        }
    }
}
