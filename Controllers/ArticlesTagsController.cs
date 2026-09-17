// ArticlesTagsController.cs
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Enums;
using GData.Services.ArticlesTags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesTagsController(IArticleTagsServices articleTagsServices) : ControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> CreateArticleTag([FromBody] ArticleTagDTO request)
        {
            var result = await articleTagsServices.CreateArticleTagService(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticleTags()
        {
            var result = await articleTagsServices.GetAllArticleTagsService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("articles/{articleId:guid}")]
        public async Task<IActionResult> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {
            var result = await articleTagsServices.GetAllArticleTagsForSpecificArticle(articleId);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> GetArticleTagById(Guid id)
        {
            var result = await articleTagsServices.GetArticleTagByIdService(id);
            return Ok(result);
        }

        [HttpGet("by-title")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> GetArticleTagByTitle([FromQuery] string title)
        {
            var result = await articleTagsServices.GetArticleTagByTitleService(title);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> EditArticleTag(Guid id, [FromBody] ArticleTagDTO request)
        {
            var result = await articleTagsServices.EditArticleTagService(id, request);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("{articleTagId:guid}/articles/{articleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> AddArticleToArticleTagList(Guid articleTagId, Guid articleId)
        {
            var result = await articleTagsServices.AddArticleToArticleTagListService(articleTagId, articleId);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{articleTagId:guid}/articles/{articleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> RemoveArticleFromArticleTagList(Guid articleTagId, Guid articleId)
        {
            var result = await articleTagsServices.RemoveArticleFromArticleTagListService(articleTagId, articleId);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> DeleteArticleTag(Guid id)
        {
            var result = await articleTagsServices.DeleteArticleTagService(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{articleTagId:guid}/posts/{postId:guid}/owners/{postOwnerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> AddPostToArticleTagList(Guid articleTagId, Guid postId, Guid postOwnerId)
        {
            var result = await articleTagsServices.AddPostToArticleTagListService(articleTagId, postId, postOwnerId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{articleTagId:guid}/posts/{postId:guid}/owners/{postOwnerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        public async Task<IActionResult> RemovePostFromArticleTagList(Guid articleTagId, Guid postId, Guid postOwnerId)
        {
            var result = await articleTagsServices.RemovePostFromArticleTagListService(articleTagId, postId, postOwnerId);
            return Ok(result);
        }
    }
}