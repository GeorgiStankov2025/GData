using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Services.Articles
{
    public interface IArticleServices
    {

        public Task<Article> CreateArticleService(Guid creatorId, ArticleDTO request);
        public Task<Article> GetArticleByIdService(Guid Id);
        public Task<Article> GetArticleByTitleService(string title);
        public Task<List<Article>> GetAllArticlesService();
        public Task<Article> EditArticleService(Guid Id, ArticleDTO request);
        public Task<Article> DeleteArticleService(Guid Id);
        public Task<Article> AddArticleToFavouriteArticlesListService(Guid articleId, Guid userId);
        public Task<Article> RemoveArticleFromFavouriteArticlesListService(Guid articleId, Guid userId);

    }
}
