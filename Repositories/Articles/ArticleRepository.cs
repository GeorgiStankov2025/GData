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
            return await dbContext.Articles.Include<Article,User>(a=>a.ArticleCreator).Include<Article, List<User>>(a => a.UsersFavoringArticle).ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid Id)
        {

            return await dbContext.Articles.Include<Article, User>(a => a.ArticleCreator).Include<Article, List<User>>(a => a.UsersFavoringArticle).FirstOrDefaultAsync(a => a.Id == Id);

        }

        public async Task<Article> GetArticleByTitle(string title)
        {

            return await dbContext.Articles.Include<Article, User>(a => a.ArticleCreator).Include<Article, List<User>>(a => a.UsersFavoringArticle).FirstOrDefaultAsync(a => a.Title==title);

        }

        public async Task<Article> RemoveArticleFromFavouriteArticlesList(Article article, User user)
        {

            user.FavouriteArticles.Remove(article);
            user.DateModified = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return article;

        }

        public async Task<Article> AddArticleToFavouriteArticlesList(Article article, User user)
        {

            user.FavouriteArticles.Add(article);
            user.DateModified = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return article;

        }

    }
}
