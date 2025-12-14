using GData.Data;
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;

namespace GData.Repositories.ArticlesTags
{
    public class ArticlesTagsRepository(GDataDbContext dbContext) : IArticlesTagsRepository
    {
        public async Task<ArticleTag> AddArticleToArticleTagList(ArticleTag articleTag, Article article)
        {
            
            articleTag.Articles.Add(article);
            articleTag.DateModified= DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return articleTag;

        }

        public async Task<ArticleTag> CreateArticleTag(ArticleTag articleTag)
        {
            
            await dbContext.AddAsync(articleTag);
            await dbContext.SaveChangesAsync();
            return articleTag;

        }

        public async Task<ArticleTag> DeleteArticleTag(ArticleTag articleTag)
        {
            
            dbContext.Remove(articleTag);
            await dbContext.SaveChangesAsync();
            return articleTag;

        }

        public async Task<ArticleTag> EditArticleTag(ArticleTag articleTag, ArticleTagDTO request)
        {
            
            articleTag.Title = request.Title;
            articleTag.DateModified= DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return articleTag;

        }

        public async Task<List<ArticleTag>> GetAllArticleTags()
        {

            return await dbContext.ArticleTags.Include<ArticleTag, List<Article>>(at => at.Articles).ToListAsync();

        }

        public async Task<ArticleTag> GetArticleTagById(Guid Id)
        {

            return await dbContext.ArticleTags.Include<ArticleTag, List<Article>>(at => at.Articles).FirstOrDefaultAsync(at=>at.Id==Id);

        }

        public async Task<ArticleTag> GetArticleTagByTitle(string title)
        {

            return await dbContext.ArticleTags.Include<ArticleTag, List<Article>>(at => at.Articles).FirstOrDefaultAsync(at => at.Title == title);

        }

        public async Task<ArticleTag> RemoveArticleFromArticleTagList(ArticleTag articleTag, Article article)
        {

            articleTag.Articles.Remove(article);
            articleTag.DateModified = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return articleTag;

        }

    }

}
