// GroupchatsMessagesController.cs
using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.GroupchatsMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupchatsMessagesController(IGroupchatsMessagesServices groupchatsMessagesServices) : ControllerBase
    {
        [Authorize]
        [HttpPost("chats/{groupChatId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        public async Task<IActionResult> CreateMessage(Guid authorId, Guid groupChatId, [FromBody] GroupchatMessageDTO request)
        {
            var result = await groupchatsMessagesServices.CreateMessageService(authorId, groupChatId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var result = await groupchatsMessagesServices.GetAllMessagesService();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("chats/{groupChatId:guid}/members/{memberId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupchatMessage>))]
        public async Task<IActionResult> GetAllMessagesInGroupChat(Guid memberId, Guid groupChatId)
        {
            var result = await groupchatsMessagesServices.GetAllMessagesInGroupChatService(memberId, groupChatId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("chats/{groupChatId:guid}/members/{memberId:guid}/users/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GroupchatMessage>))]
        public async Task<IActionResult> GetAllMessagesInGroupChatByUser(Guid memberId, Guid groupChatId, Guid userId)
        {
            var result = await groupchatsMessagesServices.GetAllMessagesInGroupChatByUserService(memberId, userId, groupChatId);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/chats/{groupChatId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        public async Task<IActionResult> EditMessage(Guid authorId, Guid groupChatId, Guid id, [FromBody] GroupchatMessageDTO request)
        {
            var result = await groupchatsMessagesServices.EditMessageService(authorId, groupChatId, id, request);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/chats/{groupChatId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupchatMessage))]
        public async Task<IActionResult> DeleteMessage(Guid authorId, Guid groupChatId, Guid id)
        {
            var result = await groupchatsMessagesServices.DeleteMessageService(authorId, groupChatId, id);
            return Ok(result);
        }
    }
}
