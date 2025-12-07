using GData.Data;
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;

namespace GData.Repositories.Articles
{
    public class ArticleRepository(GDataDbContext dbContext) : IArticleRepository
    {
        public async Task<Article> CreateArticle(Article article)
        {
            
            await dbContext.AddAsync(article);
            await dbContext.SaveChangesAsync();
            return article;

        }

        public async Task<Article> DeleteArticle(Article article)
        {
            
            dbContext.Remove(article);
            await dbContext.SaveChangesAsync();
            return article;

        }

        public async Task<Article> EditArticle(Article article, ArticleDTO request)
        {
            
            article.Title=request.Title;
            article.ArticleContent = request.Content;
            article.ArticleAuthor = request.Author;
            article.DateModified = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return article;

        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await dbContext.Articles.Include<Article,User>(a=>a.ArticleCreator).ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid Id)
        {

            return await dbContext.Articles.Include<Article, User>(a => a.ArticleCreator).FirstOrDefaultAsync(a => a.Id == Id);

        }

        public async Task<Article> GetArticleByTitle(string title)
        {

            return await dbContext.Articles.Include<Article, User>(a => a.ArticleCreator).FirstOrDefaultAsync(a => a.Title==title);

        }
    }
}
