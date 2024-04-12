using FindMe.Domain.Constants;
using FindMe.Domain.Identity;


namespace FindMe.Domain.Models
{
    public class UserDetails: BaseEntity
    {
        public string NationalId { get; set; } 
        public MatiralStatus MatiralStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public Gendre Gendre { get; set; }
        public string Job { get; set; }
        public string? PhoneNumber { get; set; } 
        public string? Notes { get; set; } 
        public string ApplicationUserId { get; set; } 
        public virtual ApplicationUser ApplicationUser { get; set; } 
    }
}
