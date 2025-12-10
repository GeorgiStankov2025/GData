using GData.Data;
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;

namespace GData.Repositories.ArticlesComments
{
    public class ArticlesCommentsRepository(GDataDbContext dbContext) : IArticlesCommentsRepository
    {
        public async Task<ArticleComment> CreateArticleComment(ArticleComment articleComment)
        {
            
            await dbContext.AddAsync(articleComment);
            await dbContext.SaveChangesAsync();
            
            return articleComment;

        }

        public async Task<ArticleComment> DeleteArticleComment(ArticleComment articleComment)
        {

            dbContext.Remove(articleComment);
            await dbContext.SaveChangesAsync();
            
            return articleComment;

        }

        public async Task<ArticleComment> EditArticleComment(ArticleComment articleComment, ArticleCommentDTO request)
        {
            
            articleComment.Content=request.CommentContent;
            articleComment.DateModified=DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            
            return articleComment;

        }

        public async Task<List<ArticleComment>> GetAllArticleComments()
        {

            return await dbContext.ArticleComments.Include<ArticleComment, User>(ac => ac.Author).Include<ArticleComment,Article>(ac=>ac.Article).ToListAsync();

        }

        public async Task<ArticleComment> GetArticleCommentById(Guid Id)
        {

            return await dbContext.ArticleComments.Include<ArticleComment, User>(ac => ac.Author).Include<ArticleComment, Article>(ac => ac.Article).FirstOrDefaultAsync(ac => ac.ArticleId == Id);

        }
    }
}
