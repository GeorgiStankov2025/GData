using GData.DTOs.PostDTO;
using GData.Entity;
using GData.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostsService postsService, ILogger<PostController> logger) : ControllerBase
    {

        [HttpGet("get-Post-By-Id{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]

        public async Task<ActionResult<Post>> GetPostById(Guid Id)
        {

            try
            {
                var result = await postsService.GetPostById(Id);
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

        [HttpGet("get-All-Posts")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {

            var result = await postsService.GetAllPosts();
            if(result.Count<1)
            {

                return NotFound();

            }
            return Ok(result);

        }

        [HttpGet("get-All-Posts-For-User{ownerId}")]
        public async Task<ActionResult<List<Post>>> GetAllPostsForUser(Guid ownerId)
        {

            var result=await postsService.GetAllPostsByUser(ownerId);
            if (result.Count < 1)
            {

                return NotFound();

            }
            return Ok(result);  

        }

        [Authorize]
        [HttpPost("create-Post{ownerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Post>> CreatePost(Guid ownerId, PostDTO request)
        {
            try
            {
                var result = await postsService.CreatePostService(ownerId, request);
                return Ok(result);
            }
            catch(UnauthorizedAccessException unauthorizedException)
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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpPatch("edit-Post{Id}")]
        public async Task<ActionResult<Post>> EditPost(Guid Id, PostDTO request)
        {

            try
            {
                var result = await postsService.UpdatePostService(request, Id);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpDelete("delete-Post{Id}")]
        public async Task<ActionResult<Post>> DeletePost(Guid Id)
        {
            try
            {
                var result = await postsService.DeletePostService(Id);
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
