using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Services.ArticlesComments
{
    public interface IArticlesCommentsServices
    {
        public Task<ArticleComment> CreateArticleCommentService(Guid authorId, Guid articleId, ArticleCommentDTO request);
        public Task<ArticleComment> EditArticleCommentService(Guid authorId, Guid articleId,Guid Id, ArticleCommentDTO request);
        public Task<ArticleComment> DeleteArticleCommentService(Guid authorId,Guid articleId,Guid Id);
        public Task<List<ArticleComment>> GetAllArticleCommentsService();
        public Task<List<ArticleComment>>GetAllArticleCommentsInArticleService(Guid articleId);
        public Task<List<ArticleComment>> GetAllArticleCommentsInArticleByUserService(Guid articleId,Guid authorId);

    }
}
