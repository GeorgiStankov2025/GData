using Org.BouncyCastle.Bcpg.OpenPgp;

namespace GData.Entity
{
    public class ArticleTag
    {
        
        public Guid Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public List<Article>? Articles { get; set; }
        public List<Post>? Posts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

    }
}
