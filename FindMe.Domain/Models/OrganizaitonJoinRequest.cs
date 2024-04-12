

namespace FindMe.Domain.Models
{
    public class OrganizaitonJoinRequest: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsApproved { get; set; }
        public DateTime RecivedAt { get; set; }
        public byte[] RequestPhoto { get; set; }
        public byte[]? Photo { get; set; }

        public OrganizaitonJoinRequest()
        {
            RecivedAt = DateTime.UtcNow;
        }


    }
}
