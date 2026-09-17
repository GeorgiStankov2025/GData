// ArticlesCommentsController.cs
using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Services.ArticlesComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesCommentsController(IArticlesCommentsServices articlesCommentsServices) : ControllerBase
    {
        [Authorize]
        [HttpPost("authors/{authorId:guid}/articles/{articleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleComment))]
        public async Task<IActionResult> CreateArticleComment(Guid authorId, Guid articleId, [FromBody] ArticleCommentDTO request)
        {
            var result = await articlesCommentsServices.CreateArticleCommentService(authorId, articleId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticleComments()
        {
            var result = await articlesCommentsServices.GetAllArticleCommentsService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("articles/{articleId:guid}")]
        public async Task<IActionResult> GetAllArticleCommentsInArticle(Guid articleId)
        {
            var result = await articlesCommentsServices.GetAllArticleCommentsInArticleService(articleId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("articles/{articleId:guid}/authors/{authorId:guid}")]
        public async Task<IActionResult> GetAllArticleCommentsInArticleByUser(Guid authorId, Guid articleId)
        {
            var result = await articlesCommentsServices.GetAllArticleCommentsInArticleByUserService(articleId, authorId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/authors/{authorId:guid}/articles/{articleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleComment))]
        public async Task<IActionResult> EditArticleComment(Guid authorId, Guid articleId, Guid id, [FromBody] ArticleCommentDTO request)
        {
            var result = await articlesCommentsServices.EditArticleCommentService(authorId, articleId, id, request);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/authors/{authorId:guid}/articles/{articleId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleComment))]
        public async Task<IActionResult> DeleteArticleComment(Guid authorId, Guid articleId, Guid id)
        {
            var result = await articlesCommentsServices.DeleteArticleCommentService(authorId, articleId, id);
            return Ok(result);
        }
    }
}