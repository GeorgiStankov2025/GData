using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Enums;
using GData.Services.ArticlesTags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesTagsController(IArticleTagsServices articleTagsServices, ILogger<ArticlesTagsController> logger) : ControllerBase
    {

        [Authorize(Roles =nameof(UserRole.Admin))]
        [HttpPost("create-ArticleTag")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> CreateArticleTag(ArticleTagDTO request)
        {
            try
            {
                var result = await articleTagsServices.CreateArticleTagService(request);
                return Ok(result);

            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Bad request");
                return Problem(

                    detail: nullException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [HttpGet("get-All-Article-Tags")]
        public async Task<ActionResult<List<ArticleTag>>> GetAllArticleTags()
        {

            var result = await articleTagsServices.GetAllArticleTagsService();

            if(result.Count<1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-All-Article-tags-For-Specific-Article{articleId}")]
        public async Task<ActionResult<List<ArticleTag>>> GetAllArticleTagsForSpecificArticle(Guid articleId)
        {

            var result = await articleTagsServices.GetAllArticleTagsForSpecificArticle(articleId);
            return Ok(result);

        }

        [HttpGet("get-Article-Tag-By-Id{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]

        public async Task<ActionResult<ArticleTag>> GetArticleTagById(Guid Id)
        {
            try
            {
             
                var result = await articleTagsServices.GetArticleTagByIdService(Id);
                return Ok(result);
            
            }
            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [HttpGet("get-Article-tag-By-Title")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> GetArticleTagByTitle(string title)
        {
            try
            {
                var result = await articleTagsServices.GetArticleTagByTitleService(title);
                return Ok(result);
            }
            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("edit-Article-Tag{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> EditArticleTag(Guid Id, ArticleTagDTO request)
        {
            try
            {
                var result = await articleTagsServices.EditArticleTagService(Id, request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("add-Articles-To-ArticleTagList{articleTagId},{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> AddArticleToArticleTagList(Guid articleTagId, Guid articleId)
        {
            try
            {
                var result = await articleTagsServices.AddArticleToArticleTagListService(articleTagId, articleId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPatch("remove-Articles-From-ArticleTagList{articleTagId},{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> RemoveArticleFromArticleTagList(Guid articleTagId, Guid articleId)
        {
            try
            {
                var result = await articleTagsServices.RemoveArticleFromArticleTagListService(articleTagId, articleId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("delete-ArticleTag{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> DeleteArticleTag(Guid Id)
        {
            try
            {
                var result = await articleTagsServices.DeleteArticleTagService(Id);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }

        }

        [Authorize]
        [HttpPatch("add-Post-To-ArticleTagList{articleTagId},{postId},{postOwnerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> AddPostToArticleTagList(Guid articleTagId, Guid postId, Guid postOwnerId)
        {
            try
            {
                var result = await articleTagsServices.AddPostToArticleTagListService(articleTagId, postId,postOwnerId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

        [Authorize]
        [HttpPatch("remove-Post-From-ArticleTagList{articleTagId},{postId},{postOwnerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleTag))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleTag>> RemovePostFromArticleTagList(Guid articleTagId, Guid postId,Guid postOwnerId)
        {
            try
            {
                var result = await articleTagsServices.RemovePostFromArticleTagListService(articleTagId, postId,postOwnerId);
                return Ok(result);
            }
            catch (UnauthorizedAccessException unauthorizedException)
            {

                logger.LogError(unauthorizedException, $"Unauthorized access");
                return Problem(

                    detail: unauthorizedException.Message,
                    title: "Unauthorized user access",
                    statusCode: StatusCodes.Status401Unauthorized,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (ArgumentNullException nullException)
            {

                logger.LogError(nullException, $"Not Found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not found!",
                    statusCode: StatusCodes.Status404NotFound,
                    instance: HttpContext.TraceIdentifier

                );

            }
            catch (FormatException formatException)
            {

                logger.LogError(formatException, $"Bad request");
                return Problem(

                    detail: formatException.Message,
                    title: "Bad request!",
                    statusCode: StatusCodes.Status400BadRequest,
                    instance: HttpContext.TraceIdentifier

                );

            }

            catch (Exception ex)
            {

                logger.LogError(ex, $"An unexpected error occured");
                return Problem(

                    detail: ex.Message,
                    title: "Internal Server Error",
                    statusCode: StatusCodes.Status500InternalServerError,
                    instance: HttpContext.TraceIdentifier

                );

            }
        }

    }
}
