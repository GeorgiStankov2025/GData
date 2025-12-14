using GData.DTOs.ArticlesDTO;
using GData.Entity;

namespace GData.Repositories.ArticlesTags
{
    public interface IArticlesTagsRepository
    {

        public Task<ArticleTag> CreateArticleTag(ArticleTag articleTag);
        public Task<ArticleTag> EditArticleTag(ArticleTag articleTag,ArticleTagDTO request);
        public Task<ArticleTag> DeleteArticleTag(ArticleTag articleTag);
        public Task<ArticleTag> GetArticleTagById(Guid Id);
        public Task<ArticleTag> GetArticleTagByTitle(string title);
        public Task<List<ArticleTag>> GetAllArticleTags();
        public Task<ArticleTag> AddArticleToArticleTagList(ArticleTag articleTag,Article article);
        public Task<ArticleTag> RemoveArticleFromArticleTagList(ArticleTag articleTag,Article article);

    }
}
