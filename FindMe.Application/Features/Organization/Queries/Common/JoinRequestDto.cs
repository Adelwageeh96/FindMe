

namespace FindMe.Application.Features.Organization.Queries.Common
{
    public class JoinRequestDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public byte[] RequestPhoto { get; set; }
    }
}
