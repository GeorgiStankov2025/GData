using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Services.PostsComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsCommentsController(IPostsCommentsService postsCommentsService, ILogger<PostsCommentsController> logger) : ControllerBase
    {

        [Authorize]
        [HttpPost("create-PostComment{authorId},{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type =typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<PostComment>> CreatePostComment(Guid authorId,Guid postId, PostCommentsDTO request)
        {
            try
            {
                var result = await postsCommentsService.CreatePostCommentService(authorId, postId, request);
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

        [HttpGet("get-All-Post-Comments")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostComments()
        {

            var result=await postsCommentsService.GetAllPostCommentsService();

            if(result.Count<1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-All-Post-Comments-For-Specific-Post{postId}")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostCommentsInPost(Guid postId)
        {

            var result= await postsCommentsService.GetAllPostCommentsInPostService(postId);

            if(result.Count<1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-All-Post-Comments-Made-By-User-In-Post{postId},{authorId}")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostCommentsInPostByUser(Guid postId, Guid authorId)
        {

            var result = await postsCommentsService.GetAllPostCommentsByUserInPostService(postId,authorId);

            if(result.Count<1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [Authorize]
        [HttpPatch("edit-Post-Comment{authorId},{postId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<PostComment>> EditPostComment(Guid authorId, Guid postId, Guid Id, PostCommentsDTO request)
        {

            try
            {
                
                var result = await postsCommentsService.EditPostCommentService(authorId, postId, Id, request);
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
        [HttpDelete("delete-Post-Comment{authorId},{postId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<PostComment>> DeletePostComment(Guid authorId,Guid postId,Guid Id)
        {

            try
            {

                var result = await postsCommentsService.DeletePostCommentService(authorId, postId, Id);
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
