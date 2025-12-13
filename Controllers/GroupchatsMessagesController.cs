using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.GroupchatsMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupchatsMessagesController(IGroupchatsMessagesServices groupchatsMessagesServices, ILogger<GroupchatsMessagesController> logger) : ControllerBase
    {

        [Authorize]
        [HttpPost("create-Message{authorId},{groupChatId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<GroupchatMessage>> CreateMessage(Guid authorId, Guid groupChatId, GroupchatMessageDTO request)
        {

            try
            {

                var result = await groupchatsMessagesServices.CreateMessageService(authorId, groupChatId, request);
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

        [HttpGet("get-All-Messages")]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessages()
        {

            var result= await groupchatsMessagesServices.GetAllMessagesService();
            return Ok(result);

        }

        [Authorize]
        [HttpGet("get-All-Messages-In-Group-Chat{memberId},{groupChatId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupchatMessage>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessagesInGroupChat(Guid memberId, Guid groupChatId)
        {
            try
            {
                var result = await groupchatsMessagesServices.GetAllMessagesInGroupChatService(memberId, groupChatId);
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

                logger.LogError(nullException, $"Not found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not Found!",
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

        [Authorize]
        [HttpGet("get-All-Messages-By-User-In-GroupChat{memberId},{groupChatId},{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupchatMessage>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessagesInGroupChatByUser(Guid memberId, Guid groupChatId,Guid userId)
        {
            try
            {
                var result = await groupchatsMessagesServices.GetAllMessagesInGroupChatByUserService(memberId, userId, groupChatId);
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

                logger.LogError(nullException, $"Not found!");
                return Problem(

                    detail: nullException.Message,
                    title: "Not Found!",
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

        [Authorize]
        [HttpPatch("edit-Message{authorId},{groupChatId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<GroupchatMessage>> EditMessage(Guid authorId,Guid groupChatId, Guid Id, GroupchatMessageDTO request)
        {
            try
            {
                var result = await groupchatsMessagesServices.EditMessageService(authorId, groupChatId, Id, request);
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
        [HttpDelete("delete-Message{authorId},{groupChatId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<GroupchatMessage>> DeleteMessage(Guid authorId, Guid groupChatId, Guid Id)
        {

            try
            {
                var result = await groupchatsMessagesServices.DeleteMessageService(authorId, groupChatId, Id);
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
