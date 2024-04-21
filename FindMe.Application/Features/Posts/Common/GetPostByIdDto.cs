

namespace FindMe.Application.Features.Posts.Common
{
    public record GetPostByIdDto
    {
        public int Id { get; set; }
        public string Descripation { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Photo { get; set; }
        public string? PhoneNumber { get; set; }
        public ActorInfromationDto Actor { get; set; }
        public List<CommentDto> Comments { get; set; }
    }

    public record CommentDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUpdated { get; set; }
        public ActorInfromationDto Actor { get; set; }
    }
}
