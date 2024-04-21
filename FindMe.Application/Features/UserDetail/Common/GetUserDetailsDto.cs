﻿

namespace FindMe.Application.Features.UserDetail.Common
{
    public class GetUserDetailsDto
    {
        public int Id { get; set; }
        public string NationalId { get; set; }
        public string MatiralStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Job { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Notes { get; set; }
    }
}
