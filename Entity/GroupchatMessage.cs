namespace GData.Entity
{
    public class GroupchatMessage
    {
        public Guid Id { get; set; }
        public required string MessageContent { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public User? Author { get; set; }
        public Guid GroupchatId { get; set; }
        public Groupchat? Groupchat { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
