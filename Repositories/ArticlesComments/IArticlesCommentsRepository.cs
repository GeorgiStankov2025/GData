using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Repositories.ArticlesComments
{
    public interface IArticlesCommentsRepository
    {
        public Task<ArticleComment> CreateArticleComment(ArticleComment articleComment);
        public Task<List<ArticleComment>> GetAllArticleComments();
        public Task<ArticleComment> GetArticleCommentById(Guid Id);
        public Task<ArticleComment> EditArticleComment(ArticleComment articleComment, ArticleCommentDTO request);
        public Task<ArticleComment> DeleteArticleComment(ArticleComment articleComment);
    }
}
