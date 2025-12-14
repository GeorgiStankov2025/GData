using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.ArticlesTags;
using GData.Services.Articles;

namespace GData.Services.ArticlesTags
{
    public class ArticleTagsServices(IArticlesTagsRepository articlesTagsRepository, IArticleServices articleServices, ArticlestagsExceptionList articlestagsExceptionList) : IArticleTagsServices
    {
        public async Task<ArticleTag> AddArticleToArticleTagListService(Guid articleTagId, Guid articleId)
        {

            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article=await articleServices.GetArticleByIdService(articleId);

            if(article is null)
            {

                return await articlestagsExceptionList.ArticleNotFound();

            }

            if(articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            await articlesTagsRepository.AddArticleToArticleTagList(articleTag, article);
            return articleTag;

        }

        public async Task<ArticleTag> CreateArticleTagService(ArticleTagDTO request)
        {

            if(string.IsNullOrWhiteSpace(request.Title))
            {

                return await articlestagsExceptionList.NoDataProvidedForArticleTag();

            }

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

            if(articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            await articlesTagsRepository.DeleteArticleTag(articleTag);
            return articleTag;

        }

        public async Task<ArticleTag> EditArticleTagService(Guid Id, ArticleTagDTO request)
        {
            
            var articleTag=await GetArticleTagByIdService(Id);

            if (string.IsNullOrWhiteSpace(request.Title))
            {

                return await articlestagsExceptionList.NoDataProvidedForArticleTag();

            }

            if (articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            await articlesTagsRepository.EditArticleTag(articleTag, request);
            return articleTag;

        }

        public async Task<List<ArticleTag>> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {
            var articleTags=await GetAllArticleTagsService();

            var article= await articleServices.GetArticleByIdService(articleId);

            if(article is null)
            {

                return await articlestagsExceptionList.ArticleNotFoundForList();

            }

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

            if(articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            return articleTag;

        }

        public async Task<ArticleTag> GetArticleTagByTitleService(string title)
        {
            
            var articleTag=await articlesTagsRepository.GetArticleTagByTitle(title);

            if (articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            return articleTag;

        }

        public async Task<ArticleTag> RemoveArticleFromArticleTagListService(Guid articleTagId, Guid articleId)
        {
            
            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article = await articleServices.GetArticleByIdService(articleId);

            if (article is null)
            {

                return await articlestagsExceptionList.ArticleNotFound();

            }

            if (articleTag is null)
            {

                return await articlestagsExceptionList.ArticleTagNotFound();

            }

            await articlesTagsRepository.RemoveArticleFromArticleTagList(articleTag, article);
            return articleTag;

        }
    }
}
