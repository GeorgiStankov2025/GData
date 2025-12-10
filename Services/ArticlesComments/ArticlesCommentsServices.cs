using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Repositories.ArticlesComments;

namespace GData.Services.ArticlesComments
{
    public class ArticlesCommentsServices(IArticlesCommentsRepository articlesCommentsRepository) : IArticlesCommentsServices
    {
        public async Task<ArticleComment> CreateArticleCommentService(Guid authorId, Guid articleId, ArticleCommentDTO request)
        {

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

            await articlesCommentsRepository.DeleteArticleComment(articleComment);
            return articleComment;

        }

        public async Task<ArticleComment> EditArticleCommentService(Guid authorId, Guid articleId, Guid Id, ArticleCommentDTO request)
        {
            
            var articleComment=await articlesCommentsRepository.GetArticleCommentById(Id);

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
