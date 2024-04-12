using FindMe.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace FindMe.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } 
        public string? Address { get; set; }
        public byte[]? Photo { get; set; } 
        public string? FCMToken { get; set; }
        public virtual UserDetails UserDetails { get; set; }
        public virtual ICollection<UserRelatives> UserRelatives { get; set; }
        public virtual List<RecognitionRequest> RecognitionRequests { get; set; }
        public virtual List<PinnedPost> PinnedPosts { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
