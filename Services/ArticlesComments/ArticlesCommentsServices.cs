using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.ArticlesComments;
using GData.Services.Articles;
using GData.Services.Users;

namespace GData.Services.ArticlesComments
{
    public class ArticlesCommentsServices(IArticlesCommentsRepository articlesCommentsRepository, IAuthServices authServices, IArticleServices articleServices, ArticleCommentsExceptionList articleCommentsExceptionList) : IArticlesCommentsServices
    {
        public async Task<ArticleComment> CreateArticleCommentService(Guid authorId, Guid articleId, ArticleCommentDTO request)
        {

            var author= await authServices.GetUserByIdService(authorId);

            var article = await articleServices.GetArticleByIdService(articleId);

            if(author is null)
            {

                return await articleCommentsExceptionList.CreateArticleCommentAuthorDoesNotExist();

            }

            if (article is null)
            {

                return await articleCommentsExceptionList.CreateArticleCommentArticleDoesNotExist();

            }

            if(string.IsNullOrWhiteSpace(request.CommentContent))
            {

                return await articleCommentsExceptionList.NoContentHasBeenProvidedForArticleComment();

            }

            if(request.CommentContent.Length<3)
            {

                return await articleCommentsExceptionList.ContentNeedsToHaveMoreThanThreeChars();

            }

            if(author.IsEmailConfirmed is false)
            {

                return await articleCommentsExceptionList.UnverifiedAuthor();

            }

            var articleComment = new ArticleComment()
            {
                Content = request.CommentContent,
                AuthorId = authorId,
                ArticleId = articleId,
                DateCreated= DateTime.UtcNow,
            };

            await articlesCommentsRepository.CreateArticleComment(articleComment);
            return articleComment;

        }

        public async Task<ArticleComment> DeleteArticleCommentService(Guid authorId, Guid articleId, Guid Id)
        {
            
            var articleComment=await articlesCommentsRepository.GetArticleCommentById(Id);

            var article= await articleServices.GetArticleByIdService(articleId);

            var author = await authServices.GetUserByIdService(authorId);

            if (articleComment is null)
            {

                return await articleCommentsExceptionList.ArticleCommentDoesNotExist();

            }

            if (articleComment.ArticleId != articleId)
            {

                return await articleCommentsExceptionList.ArticleNotValid();

            }

            if (articleComment.AuthorId != authorId)
            {

                return await articleCommentsExceptionList.AuthorNotValid();

            }

            if (article is null)
            {

                return await articleCommentsExceptionList.EditArticleCommentArticleDoesNotExist();

            }

            if (author is null)
            {

                return await articleCommentsExceptionList.EditArticleCommentAuthorDoesNotExist();

            }

            await articlesCommentsRepository.DeleteArticleComment(articleComment);
            return articleComment;

        }

        public async Task<ArticleComment> EditArticleCommentService(Guid authorId, Guid articleId, Guid Id, ArticleCommentDTO request)
        {
            
            var articleComment=await articlesCommentsRepository.GetArticleCommentById(Id);

            var author = await authServices.GetUserByIdService(authorId);

            var article= await articleServices.GetArticleByIdService(articleId);

            if(articleComment is null)
            {

                return await articleCommentsExceptionList.ArticleCommentDoesNotExist();

            }

            if (articleComment.ArticleId != articleId)
            {

                return await articleCommentsExceptionList.ArticleNotValid();

            }

            if (articleComment.AuthorId != authorId)
            {

                return await articleCommentsExceptionList.AuthorNotValid();

            }

            if(article is null)
            {

                return await articleCommentsExceptionList.EditArticleCommentArticleDoesNotExist();

            }

            if(author is null)
            {

                return await articleCommentsExceptionList.EditArticleCommentAuthorDoesNotExist();

            }

            if(string.IsNullOrWhiteSpace(request.CommentContent))
            {

                return await articleCommentsExceptionList.NoContentHasBeenProvidedForArticleComment();

            }

            if(request.CommentContent.Length<3)
            {

                return await articleCommentsExceptionList.ContentNeedsToHaveMoreThanThreeChars();

            }

            if(author.IsEmailConfirmed is false)
            {

                return await articleCommentsExceptionList.UnverifiedAuthor();

            }

            await articlesCommentsRepository.EditArticleComment(articleComment, request);
            return articleComment;

        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsInArticleByUserService(Guid articleId, Guid authorId)
        {
            List<ArticleComment> selectedArticles = new List<ArticleComment>();

            var articleComments = await articlesCommentsRepository.GetAllArticleComments();

            foreach (var articleComment in articleComments)
            {

                if (articleComment.ArticleId == articleId&&articleComment.AuthorId==authorId)
                {

                    selectedArticles.Add(articleComment);

                }

            }

            return selectedArticles;
        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsInArticleService(Guid articleId)
        {

            List<ArticleComment> selectedArticles = new List<ArticleComment>();

            var articleComments = await articlesCommentsRepository.GetAllArticleComments();

            foreach (var articleComment in articleComments)
            {

                if(articleComment.ArticleId == articleId)
                {

                    selectedArticles.Add(articleComment);

                }

            }

            return selectedArticles;

        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsService()
        {

            return await articlesCommentsRepository.GetAllArticleComments();

        }
    }
}
