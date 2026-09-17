// ArticleController.cs
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Enums;
using GData.Services.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(IArticleServices articleServices) : ControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateArticle(Guid creatorId, [FromBody] ArticleDTO request)
        {
            var result = await articleServices.CreateArticleService(creatorId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticles()
        {
            var result = await articleServices.GetAllArticlesService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var result = await articleServices.GetArticleByIdService(id);
            return Ok(result);
        }

        [HttpGet("by-title")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetArticleByTitle([FromQuery] string title)
        {
            var result = await articleServices.GetArticleByTitleService(title);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditArticle(Guid id, [FromBody] ArticleDTO request)
        {
            var result = await articleServices.EditArticleService(id, request);
            return Ok(result);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var result = await articleServices.DeleteArticleService(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{articleId:guid}/favourites/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        public async Task<IActionResult> AddArticleToFavourites(Guid articleId, Guid userId)
        {
            var result = await articleServices.AddArticleToFavouriteArticlesListService(articleId, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{articleId:guid}/favourites/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Article))]
        public async Task<IActionResult> RemoveArticleFromFavourites(Guid articleId, Guid userId)
        {
            var result = await articleServices.RemoveArticleFromFavouriteArticlesListService(articleId, userId);
            return Ok(result);
        }
    }
}