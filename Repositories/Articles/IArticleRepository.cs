using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Repositories.Articles
{
    public interface IArticleRepository
    {
        public Task<Article> CreateArticle(Article article);
        public Task<Article> GetArticleById(Guid Id);
        public Task<Article> GetArticleByTitle(string title);
        public Task<List<Article>> GetAllArticles();
        public Task<Article> EditArticle(Article article, ArticleDTO request);
        public Task<Article> DeleteArticle(Article article);
        public Task<Article> AddArticleToFavouriteArticlesList(Article article, User user);
        public Task<Article> RemoveArticleFromFavouriteArticlesList(Article article, User user);

    }
}
