using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.Groupchats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatsController(IGroupChatsServices groupChatsServices, ILogger<GroupChatsController> logger) : ControllerBase
    {

        [Authorize]
        [HttpPost("create-GroupChat{creatorId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]   
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> CreateGroupChat(Guid creatorId, GroupchatDTO request)
        {
            try
            {
                
                var result = await groupChatsServices.CreateGroupChatService(creatorId, request);
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

        [HttpGet("get-All-GroupChats")]
        public async Task<ActionResult<List<Groupchat>>> GetAllGroupChats()
        {

            var result= await groupChatsServices.GetAllGroupChatsService();

            if(result.Count<1)
            {

                return NoContent();

            }

            return Ok(result);

        }

        [HttpGet("get-GroupChat-By-Id{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> GetGroupChatById(Guid Id)
        {

            try
            {
                var result = await groupChatsServices.GetGroupChatByIdService(Id);
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

        [HttpGet("get-GroupChat-By-ChatName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> GetGroupChatByChatName(string chatName)
        {
            try
            {
            
                var result = await groupChatsServices.GetGroupChatByChatNameService(chatName);
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

        [Authorize]
        [HttpPatch("add-Chat-Member{creatorId},{userId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))] 
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> AddMemberToGroupChat(Guid creatorId,Guid userId,Guid Id)
        {

            try
            {
             
                var result = await groupChatsServices.AddUserToGroupChatService(creatorId, userId, Id);
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
        [HttpPatch("edit-GropuChat-Title{creatorId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> EditGroupChatTitle(Guid creatorId,Guid Id,GroupchatDTO request)
        {
            try
            {
                var result = await groupChatsServices.EditGroupChatTitleService(creatorId, Id, request);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [HttpPatch("remove-User-From-GroupChat{creatorId},{userId},{Id}")]
        public async Task<ActionResult<Groupchat>> RemoveUserFromGroupChat(Guid creatorId,Guid userId,Guid Id)
        {
            try
            {
             
                var result = await groupChatsServices.RemoveUserFromGroupChatService(creatorId, userId, Id);
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
        [HttpDelete("delete-GroupChat{creatorId},{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]  
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<ActionResult<Groupchat>> DeleteGroupChat(Guid creatorId,Guid Id)
        {

            try
            {
                var result = await groupChatsServices.DeleteGroupChatService(creatorId, Id);
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
