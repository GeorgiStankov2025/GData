using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Repositories.Articles;
using GData.Repositories.Users;

namespace GData.Services.Articles
{
    public class ArticleServices(IArticleRepository articleRepository) : IArticleServices
    {
        public async Task<Article> CreateArticleService(Guid creatorId, ArticleDTO request)
        {

            var article = new Article()
            {

                Title = request.Title,
                ArticleContent = request.Content,
                ArticleAuthor = request.Author,
                CreatorId = creatorId,
                DateCreated = DateTime.UtcNow,

            };

            await articleRepository.CreateArticle(article);
            return article;

        }

        public async Task<Article> DeleteArticleService(Guid creatorId, Guid Id)
        {

            var article = await GetArticleByIdService(Id);

            var result=await articleRepository.DeleteArticle(article);

            return result;

        }

        public async Task<Article> EditArticleService(Guid creatorId, Guid Id, ArticleDTO request)
        {

            var article = await GetArticleByIdService(Id);

            var result=await articleRepository.EditArticle(article, request);

            return result;

        }

        public async Task<List<Article>> GetAllArticlesService()
        {

            return await articleRepository.GetAllArticles();

        }

        public async Task<Article> GetArticleByIdService(Guid Id)
        {

            var article = await articleRepository.GetArticleById(Id);
            return article;

        }

        public async Task<Article> GetArticleByTitleService(string title)
        {
            
            var article= await articleRepository.GetArticleByTitle(title);
            return article;

        }
    }
}
