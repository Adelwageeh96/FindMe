using FindMe.Domain.Identity;
namespace FindMe.Domain.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsUpdated { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Comment() 
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
