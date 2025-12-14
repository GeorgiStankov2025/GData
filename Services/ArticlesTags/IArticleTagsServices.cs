using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Services.ArticlesTags
{
    public interface IArticleTagsServices
    {

        public Task<ArticleTag> CreateArticleTagService(ArticleTagDTO request);
        public Task<ArticleTag> EditArticleTagService(Guid Id,ArticleTagDTO request);
        public Task<ArticleTag> DeleteArticleTagService(Guid Id);
        public Task<ArticleTag> GetArticleTagByIdService(Guid Id);
        public Task<ArticleTag> GetArticleTagByTitleService(string title);
        public Task<List<ArticleTag>> GetAllArticleTagsService();
        public Task<List<ArticleTag>> GetAllArticleTagsForSpecificArticle(Guid articleId);
        public Task<ArticleTag> AddArticleToArticleTagListService(Guid articleTagId,Guid articleId);
        public Task<ArticleTag> RemoveArticleFromArticleTagListService(Guid articleTagId, Guid articleId);

    }
}
