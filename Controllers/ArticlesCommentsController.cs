using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Services.ArticlesComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesCommentsController(IArticlesCommentsServices articlesCommentsServices, ILogger<ArticlesCommentsController> logger) : ControllerBase
    {

        [Authorize]
        [HttpPost("create-ArticleComment{authorId},{articleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleComment>> CreateArticleComment(Guid authorId,Guid articleId,ArticleCommentDTO request)
        {
            try
            {
                var result = await articlesCommentsServices.CreateArticleCommentService(authorId, articleId, request);
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

        [HttpGet("get-All-ArticleComments")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleComments()
        {

            var result = await articlesCommentsServices.GetAllArticleCommentsService();

            if (result.Count < 1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-All-ArticleComments-InArticle{articleId}")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleCommentsInArticle(Guid articleId)
        {

            var result= await articlesCommentsServices.GetAllArticleCommentsInArticleService(articleId);

            if (result.Count < 1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-All-ArticleComments-ByUser-InArticle{authorId},{articleId}")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleCommentsInArticleByUser(Guid authorId,Guid articleId)
        {

            var result = await articlesCommentsServices.GetAllArticleCommentsInArticleByUserService(articleId,authorId);

            if (result.Count < 1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [Authorize]
        [HttpPatch("edit-ArticleComment{authorId},{articleId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArticleComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleComment>> EditArticleComment(Guid authorId, Guid articleId, Guid Id, ArticleCommentDTO request)
        {

            try
            {

                var result = await articlesCommentsServices.EditArticleCommentService(authorId, articleId, Id, request);
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
        [HttpDelete("delete-Article-Comment{authorId},{articleId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<ArticleComment>> DeleteArticleComment(Guid authorId, Guid articleId, Guid Id)
        {

            try
            {
                var result = await articlesCommentsServices.DeleteArticleCommentService(authorId, articleId, Id);
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
