using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.Articles;
using GData.Repositories.Users;
using GData.Services.Users;

namespace GData.Services.Articles
{
    public class ArticleServices(IArticleRepository articleRepository, IAuthServices authServices) : IArticleServices
    {
        public async Task<Article> CreateArticleService(Guid creatorId, ArticleDTO request)
        {

            var creator = await authServices.GetUserByIdService(creatorId);

            if (creator is null)
            {
                throw new NotFoundException("User not found!");
            }

            if (creator.IsEmailConfirmed == false)
            {
                throw new BadRequestException("Unverified email");
            }

            if (string.IsNullOrWhiteSpace(request.Content) || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Author))
            {
                throw new BadRequestException("Title is required!");
            }

            if (string.IsNullOrWhiteSpace(request.Content) && string.IsNullOrWhiteSpace(request.Title) && string.IsNullOrWhiteSpace(request.Author))
            {
                throw new BadRequestException("Content is required!");
            }

            if (request.Content.Length < 4 || request.Title.Length < 4 || request.Author.Length < 4)
            {
                throw new BadRequestException("Title and author should be at least 4 characters long");
            }

            if (request.Content.Length < 4 && request.Title.Length < 4 && request.Author.Length < 4)
            {
                throw new BadRequestException("Title and author should be at least 4 characters long");
            }

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

        public async Task<Article> DeleteArticleService(Guid Id)
        {
            var article = await GetArticleByIdService(Id);

            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }
            var result = await articleRepository.DeleteArticle(article);
            return result;

        }

        public async Task<Article> EditArticleService(Guid Id, ArticleDTO request)
        {
            var article = await GetArticleByIdService(Id);
            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }

            if (article.ArticleCreator is null)
            {
                throw new NotFoundException("Article creator not found!");
            }

            if (request.Content.Length < 4 && request.Title.Length < 4 && request.Author.Length < 4)
            {
                throw new BadRequestException("Title and author should be at least 4 characters long");
            }

            if (request.Content.Length < 4 || request.Title.Length < 4 || request.Author.Length < 4)
            {
                throw new BadRequestException("Title and author should be at least 4 characters long");
            }

            if (string.IsNullOrWhiteSpace(request.Author) || string.IsNullOrWhiteSpace(request.Content) || string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadRequestException("All fields are required!");
            }

            if (string.IsNullOrWhiteSpace(request.Author) && string.IsNullOrWhiteSpace(request.Content) && string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadRequestException("All fields are required!");
            }
            var result = await articleRepository.EditArticle(article, request);
            return result;
        }

        public async Task<List<Article>> GetAllArticlesService()
        {
            return await articleRepository.GetAllArticles();
        }

        public async Task<Article> GetArticleByIdService(Guid Id)
        {
            var article = await articleRepository.GetArticleById(Id);
            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }
            return article;

        }

        public async Task<Article> GetArticleByTitleService(string title)
        {
            var article = await articleRepository.GetArticleByTitle(title);
            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }
            return article;
        }

        public async Task<Article> AddArticleToFavouriteArticlesListService(Guid articleId, Guid userId)
        {
            var article = await GetArticleByIdService(articleId);
            var user = await authServices.GetUserByIdService(userId);
            await articleRepository.AddArticleToFavouriteArticlesList(article, user);
            return article;
        }

        public async Task<Article> RemoveArticleFromFavouriteArticlesListService(Guid articleId, Guid userId)
        {
            var article = await GetArticleByIdService(articleId);
            var user = await authServices.GetUserByIdService(userId);
            await articleRepository.RemoveArticleFromFavouriteArticlesList(article, user);
            return article;
        }
    }
}
