using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.ArticlesComments;
using GData.Services.Articles;
using GData.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace GData.Services.ArticlesComments
{
    public class ArticlesCommentsServices(IArticlesCommentsRepository articlesCommentsRepository, IAuthServices authServices, IArticleServices articleServices) : IArticlesCommentsServices
    {
        public async Task<ArticleComment> CreateArticleCommentService(Guid authorId, Guid articleId, ArticleCommentDTO request)
        {

            var author = await authServices.GetUserByIdService(authorId);

            var article = await articleServices.GetArticleByIdService(articleId);

            if (author is null)
            {
                throw new NotFoundException("Author not found");
            }

            if (article is null)
            {
                throw new NotFoundException("Article not found");
            }

            if (string.IsNullOrWhiteSpace(request.CommentContent))
            {
                throw new BadRequestException("Comment content is required.");
            }

            if (request.CommentContent.Length < 3)
            {
                throw new NotFoundException("Comment content is required at least three characters long.");
            }

            if (author.IsEmailConfirmed is false)
            {
                throw new BadRequestException("Invalid author.");
            }

            var articleComment = new ArticleComment()
            {
                Content = request.CommentContent,
                AuthorId = authorId,
                ArticleId = articleId,
                DateCreated = DateTime.UtcNow,
            };

            await articlesCommentsRepository.CreateArticleComment(articleComment);
            return articleComment;

        }

        public async Task<ArticleComment> DeleteArticleCommentService(Guid authorId, Guid articleId, Guid Id)
        {

            var articleComment = await articlesCommentsRepository.GetArticleCommentById(Id);

            var article = await articleServices.GetArticleByIdService(articleId);

            var author = await authServices.GetUserByIdService(authorId);

            if (articleComment is null)
            {
                throw new NotFoundException("Article comment not found");
            }

            if (articleComment.ArticleId != articleId)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (articleComment.AuthorId != authorId)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (article is null)
            {
                throw new NotFoundException("Article not found");
            }

            if (author is null)
            {
                throw new NotFoundException("Author not found");
            }
            await articlesCommentsRepository.DeleteArticleComment(articleComment);
            return articleComment;

        }

        public async Task<ArticleComment> EditArticleCommentService(Guid authorId, Guid articleId, Guid Id, ArticleCommentDTO request)
        {

            var articleComment = await articlesCommentsRepository.GetArticleCommentById(Id);

            var author = await authServices.GetUserByIdService(authorId);

            var article = await articleServices.GetArticleByIdService(articleId);

            if (articleComment is null)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (articleComment.ArticleId != articleId)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (articleComment.AuthorId != authorId)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (article is null)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (author is null)
            {
                throw new BadRequestException("Cannot modify article");
            }

            if (string.IsNullOrWhiteSpace(request.CommentContent))
            {
                throw new BadRequestException("Comment content is required.");
            }

            if (request.CommentContent.Length < 3)
            {
                throw new BadRequestException("Comment content is required to have more than three characters.");
            }

            if (author.IsEmailConfirmed is false)
            {
                throw new BadRequestException("Cannot modify article");
            }

            await articlesCommentsRepository.EditArticleComment(articleComment, request);
            return articleComment;

        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsInArticleByUserService(Guid articleId, Guid authorId)
        {
            var articleComments = await articlesCommentsRepository.GetAllArticleComments();
            return articleComments.Where(ac => ac.ArticleId == articleId && ac.AuthorId == authorId).ToList();
        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsInArticleService(Guid articleId)
        {
            var articleComments = await articlesCommentsRepository.GetAllArticleComments();
            return articleComments.Where(ac => ac.ArticleId == articleId).ToList();
        }

        public async Task<List<ArticleComment>> GetAllArticleCommentsService()
        {
            return await articlesCommentsRepository.GetAllArticleComments();
        }
    }
}
