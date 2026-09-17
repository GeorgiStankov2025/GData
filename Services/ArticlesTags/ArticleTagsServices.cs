using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.ArticlesTags;
using GData.Services.Articles;
using GData.Services.Posts;
using GData.Services.Users;

namespace GData.Services.ArticlesTags
{
    public class ArticleTagsServices(IArticlesTagsRepository articlesTagsRepository, IAuthServices authServices, IPostsService postsService, IArticleServices articleServices) : IArticleTagsServices
    {
        public async Task<ArticleTag> AddArticleToArticleTagListService(Guid articleTagId, Guid articleId)
        {

            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article = await articleServices.GetArticleByIdService(articleId);

            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            await articlesTagsRepository.AddArticleToArticleTagList(articleTag, article);
            return articleTag;

        }

        public async Task<ArticleTag> AddPostToArticleTagListService(Guid articleTagId, Guid postId, Guid postOwnerId)
        {
            var articleTag = await GetArticleTagByIdService(articleTagId);
            var post = await postsService.GetPostById(postId);
            var postOwner = await authServices.GetUserByIdService(postOwnerId);

            if (post is null)
            {
                throw new NotFoundException("Article not found!");
            }

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            if (postOwner != post.Owner)
            {
                throw new BadRequestException("Invalid owner");
            }

            await articlesTagsRepository.AddPostToArticleTagList(articleTag, post);
            return articleTag;
        }

        public async Task<ArticleTag> CreateArticleTagService(ArticleTagDTO request)
        {

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadRequestException("Title is required!");
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

            var articleTag = await GetArticleTagByIdService(Id);

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            await articlesTagsRepository.DeleteArticleTag(articleTag);
            return articleTag;

        }

        public async Task<ArticleTag> EditArticleTagService(Guid Id, ArticleTagDTO request)
        {

            var articleTag = await GetArticleTagByIdService(Id);

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new BadRequestException("Title is required!");
            }

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            await articlesTagsRepository.EditArticleTag(articleTag, request);
            return articleTag;

        }

        public async Task<List<ArticleTag>> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {
            var articleTags = await GetAllArticleTagsService();

            var article = await articleServices.GetArticleByIdService(articleId);

            if (article is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            List<ArticleTag> selectedTags = new List<ArticleTag>();

            foreach (var articleTag in articleTags)
            {
                if (articleTag.Articles.Contains(article))
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
            var articleTag = await articlesTagsRepository.GetArticleTagById(Id);

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            return articleTag;

        }

        public async Task<ArticleTag> GetArticleTagByTitleService(string title)
        {

            var articleTag = await articlesTagsRepository.GetArticleTagByTitle(title);

            if (articleTag is null)
            {

                throw new NotFoundException("Article tag not found!");

            }

            return articleTag;

        }

        public async Task<ArticleTag> RemoveArticleFromArticleTagListService(Guid articleTagId, Guid articleId)
        {

            var articleTag = await GetArticleTagByIdService(articleTagId);
            var article = await articleServices.GetArticleByIdService(articleId);

            if (article is null)
            {
                throw new NotFoundException("Article not found!");
            }

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            await articlesTagsRepository.RemoveArticleFromArticleTagList(articleTag, article);
            return articleTag;

        }

        public async Task<ArticleTag> RemovePostFromArticleTagListService(Guid articleTagId, Guid postId, Guid postOwnerId)
        {

            var articleTag = await GetArticleTagByIdService(articleTagId);
            var post = await postsService.GetPostById(postId);
            var postOwner = await authServices.GetUserByIdService(postOwnerId);

            if (post is null)
            {
                throw new NotFoundException("Post not found.");
            }

            if (articleTag is null)
            {
                throw new NotFoundException("Article tag not found!");
            }

            if (postOwner != post.Owner)
            {
                throw new BadRequestException("Cannot edit article!");
            }

            await articlesTagsRepository.RemovePostFromArticleTagList(articleTag, post);
            return articleTag;

        }
    }
}
