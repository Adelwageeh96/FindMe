using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMe.Application.Features.Posts.Common
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Descripation { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Photo { get; set; }
        public string? PhoneNumber { get; set; }
        public ActorInfromationDto Actor { get; set; }
       
    }

    public class ActorInfromationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
    }
}
