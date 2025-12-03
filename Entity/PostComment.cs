namespace GData.Entity
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public required string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public User? Author { get; set; }
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
