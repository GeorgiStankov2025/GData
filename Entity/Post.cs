namespace GData.Entity
{
    public class Post
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }=string.Empty;

        public Guid OwnerId { get; set; }

        public User? Owner { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

    }
}
