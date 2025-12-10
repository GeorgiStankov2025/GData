namespace GData.Entity
{
    public class ArticleComment
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }=string.Empty;
        public Guid AuthorId { get; set; }
        public User? Author { get; set; }
        public Guid ArticleId { get; set; }
        public Article? Article { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
