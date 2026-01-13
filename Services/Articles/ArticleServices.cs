using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.Articles;
using GData.Repositories.Users;
using GData.Services.Users;

namespace GData.Services.Articles
{
    public class ArticleServices(IArticleRepository articleRepository, IAuthServices authServices, ArticlesExceptionList articlesExceptionList) : IArticleServices
    {
        public async Task<Article> CreateArticleService(Guid creatorId, ArticleDTO request)
        {

            var creator = await authServices.GetUserByIdService(creatorId);

            if (creator is null)
            {

                return await articlesExceptionList.ArticleCreatorDoesNotExist();

            }

            if (creator.UserRole != 0)
            {

                return await articlesExceptionList.InvalidUser();

            }

            if (creator.IsEmailConfirmed == false)
            {

                return await articlesExceptionList.UnverifiedUserEmail();

            }

            if (string.IsNullOrWhiteSpace(request.Content) || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Author))
            {

                return await articlesExceptionList.NoDataProvidedForArticle();

            }

            if (string.IsNullOrWhiteSpace(request.Content) && string.IsNullOrWhiteSpace(request.Title) && string.IsNullOrWhiteSpace(request.Author))
            {

                return await articlesExceptionList.NoDataProvidedForArticle();

            }

            if (request.Content.Length < 4 || request.Title.Length < 4 || request.Author.Length < 4)
            {

                return await articlesExceptionList.BadDataFormat();

            }

            if (request.Content.Length < 4 && request.Title.Length < 4 && request.Author.Length < 4)
            {

                return await articlesExceptionList.BadDataFormat();

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

                return await articlesExceptionList.ArticleNotFound();

            }

            var result = await articleRepository.DeleteArticle(article);

            return result;

        }

        public async Task<Article> EditArticleService(Guid Id, ArticleDTO request)
        {

            var article = await GetArticleByIdService(Id);

            if (article is null)
            {

                return await articlesExceptionList.ArticleNotFound();

            }

            if (article.ArticleCreator is null)
            {

                return await articlesExceptionList.EditArticleCreatorDoesNotExist();

            }

            if (request.Content.Length < 4 && request.Title.Length < 4 && request.Author.Length < 4)
            {

                return await articlesExceptionList.BadDataFormat();

            }

            if (request.Content.Length < 4 || request.Title.Length < 4 || request.Author.Length < 4)
            {

                return await articlesExceptionList.BadDataFormat();

            }

            if (string.IsNullOrWhiteSpace(request.Author)||string.IsNullOrWhiteSpace(request.Content)||string.IsNullOrWhiteSpace(request.Title))
            {

                return await articlesExceptionList.NoDataProvidedForArticle();

            }

            if (string.IsNullOrWhiteSpace(request.Author) && string.IsNullOrWhiteSpace(request.Content) && string.IsNullOrWhiteSpace(request.Title))
            {

                return await articlesExceptionList.NoDataProvidedForArticle();

            }

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
           
            if(article is null)
            {

                return await articlesExceptionList.ArticleNotFound();

            }

            return article;

        }

        public async Task<Article> GetArticleByTitleService(string title)
        {
            
            var article= await articleRepository.GetArticleByTitle(title);

            if (article is null)
            {

                return await articlesExceptionList.ArticleNotFound();

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
