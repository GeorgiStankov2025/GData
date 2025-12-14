using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Repositories.ArticlesTags;
using GData.Services.Articles;

namespace GData.Services.ArticlesTags
{
    public class ArticleTagsServices(IArticlesTagsRepository articlesTagsRepository, IArticleServices articleServices) : IArticleTagsServices
    {
        public async Task<ArticleTag> AddArticleToArticleTagListService(Guid articleTagId, Guid articleId)
        {

            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article=await articleServices.GetArticleByIdService(articleId);

            await articlesTagsRepository.AddArticleToArticleTagList(articleTag, article);
            return articleTag;

        }

        public async Task<ArticleTag> CreateArticleTagService(ArticleTagDTO request)
        {

            var articleTag = new ArticleTag()
            {
                
                Title = request.Title,
                DateCreated = DateTime.UtcNow,

            };

            await articlesTagsRepository.CreateArticleTag(articleTag);
            return articleTag;

        }

        public async Task<ArticleTag> DeleteArticleTagService(Guid Id)
        {
            
            var articleTag= await GetArticleTagByIdService(Id);
            await articlesTagsRepository.DeleteArticleTag(articleTag);
            return articleTag;

        }

        public async Task<ArticleTag> EditArticleTagService(Guid Id, ArticleTagDTO request)
        {
            
            var articleTag=await GetArticleTagByIdService(Id);

            await articlesTagsRepository.EditArticleTag(articleTag, request);
            return articleTag;

        }

        public async Task<List<ArticleTag>> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {
            var articleTags=await GetAllArticleTagsService();

            var article= await articleServices.GetArticleByIdService(articleId);

            List<ArticleTag> selectedTags = new List<ArticleTag>();

            foreach (var articleTag in articleTags)
            {

                if(articleTag.Articles.Contains(article))
                {

                    selectedTags.Add(articleTag);

                }

            }

            return selectedTags;

        }

        public async Task<List<ArticleTag>> GetAllArticleTagsService()
        {
            
            return await articlesTagsRepository.GetAllArticleTags();

        }

        public async Task<ArticleTag> GetArticleTagByIdService(Guid Id)
        {
            
            var articleTag=await articlesTagsRepository.GetArticleTagById(Id);
            return articleTag;

        }

        public async Task<ArticleTag> GetArticleTagByTitleService(string title)
        {
            
            var articleTag=await articlesTagsRepository.GetArticleTagByTitle(title);
            return articleTag;

        }

        public async Task<ArticleTag> RemoveArticleFromArticleTagListService(Guid articleTagId, Guid articleId)
        {
            
            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article = await articleServices.GetArticleByIdService(articleId);

            await articlesTagsRepository.RemoveArticleFromArticleTagList(articleTag, article);
            return articleTag;

        }
    }
}
