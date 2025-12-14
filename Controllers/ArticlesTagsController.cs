using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Services.ArticlesTags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesTagsController(IArticleTagsServices articleTagsServices) : ControllerBase
    {

        [HttpPost("create-ArticleTag")]
        public async Task<ActionResult<ArticleTag>> CreateArticleTag(ArticleTagDTO request)
        {

            var result = await articleTagsServices.CreateArticleTagService(request);
            return Ok(result);

        }

        [HttpGet("get-All-Article-Tags")]
        public async Task<ActionResult<List<ArticleTag>>> GetAllArticleTags()
        {

            var result = await articleTagsServices.GetAllArticleTagsService();
            return Ok(result);

        }

        [HttpGet("get-All-Article-tags-For-Specific-Article{articleId}")]
        public async Task<ActionResult<List<ArticleTag>>> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {

            var result = await articleTagsServices.GetAllArticleTagsForSpecificArticle(articleId);
            return Ok(result);

        }

        [HttpGet("get-Article-Tag-By-Id{Id}")]
        public async Task<ActionResult<ArticleTag>> GetArticleTagById(Guid Id)
        {

            var result = await articleTagsServices.GetArticleTagByIdService(Id);
            return Ok(result);

        }

        [HttpGet("get-Article-tag-By-Title")]
        public async Task<ActionResult<ArticleTag>> GetArticleTagByTitle(string title)
        {

            var result = await articleTagsServices.GetArticleTagByTitleService(title);
            return Ok(result);

        }

        [HttpPatch("edit-Article-Tag{Id}")]
        public async Task<ActionResult<ArticleTag>> EditArticleTag(Guid Id, ArticleTagDTO request)
        {

            var result = await articleTagsServices.EditArticleTagService(Id, request);
            return Ok(result);

        }

        [HttpPatch("add-Articles-To-ArticleTagList{articleTagId},{articleId}")]
        public async Task<ActionResult<ArticleTag>> AddArticleToArticleTagList(Guid articleTagId, Guid articleId)
        {

            var result = await articleTagsServices.AddArticleToArticleTagListService(articleTagId, articleId);
            return Ok(result);

        }

        [HttpPatch("remove-Articles-From-ArticleTagList{articleTagId},{articleId}")]
        public async Task<ActionResult<ArticleTag>> RemoveArticleFromArticleTagList(Guid articleTagId, Guid articleId)
        {

            var result = await articleTagsServices.RemoveArticleFromArticleTagListService(articleTagId, articleId);
            return Ok(result);

        }

        [HttpDelete("delete-ArticleTag{Id}")]
        public async Task<ActionResult<ArticleTag>> DeleteArticleTag(Guid Id)
        {

            var result=await articleTagsServices.DeleteArticleTagService(Id);
            return Ok(result);

        }

    }
}
