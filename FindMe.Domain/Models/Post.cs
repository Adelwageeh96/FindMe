using FindMe.Domain.Identity;


namespace FindMe.Domain.Models
{
    public class Post : BaseEntity
    {
        public string Descripation { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Photo { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public Post()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
