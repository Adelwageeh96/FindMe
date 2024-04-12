using FindMe.Domain.Constants;
using FindMe.Domain.Identity;

namespace FindMe.Domain.Models
{
    public class UserRelatives: BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNumber1 { get; set; }
        public Gendre Gendre { get; set; }
        public Relationship Relationship { get; set; }
        public string? PhoneNumber2 { get; set;}
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
