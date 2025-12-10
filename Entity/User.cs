using GData.Enums;
using System.Text.Json.Serialization;

namespace GData.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        public required string Username { get; set; }=string.Empty;

        public string PasswordHash {  get; set; }=string.Empty;

        public required string Email { get; set; } =string.Empty;

        public required string Firstname {  get; set; }=string.Empty;

        public required string Lastname { get; set;}=string.Empty;

        public UserRole UserRole { get; set; } = UserRole.User;

        public bool IsEmailConfirmed { get; set; } = false;

        public int VerificationCode { get; set; } = 0;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [JsonIgnore]
        public List<Post>? UserPosts { get; set; }

        [JsonIgnore]
        public List<PostComment>? PostComments { get; set; }

        [JsonIgnore]
        public List<Article>? UserArticles { get; set; }

        [JsonIgnore]
        public List<ArticleComment>? ArticleComments { get; set; }

        public List<Groupchat>? UserGroupchats { get; set; } 

    }
}
