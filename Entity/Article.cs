using System.Text.Json.Serialization;

namespace GData.Entity
{
    public class Article
    {

        public Guid Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public required string ArticleContent {  get; set; } = string.Empty;
        public required string ArticleAuthor {  get; set; } = string.Empty;
        public Guid CreatorId { get; set; }
        public User? ArticleCreator { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        
        [JsonIgnore]
        public List<ArticleComment>? ArticleComments { get; set; }

        [JsonIgnore]
        public List<ArticleTag>? ArticleTags { get; set; }

        [JsonIgnore]
        public List<User>? UsersFavoringArticle { get; set; } 

    }
}
