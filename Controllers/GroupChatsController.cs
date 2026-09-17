// GroupChatsController.cs
using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.Groupchats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatsController(IGroupChatsServices groupChatsServices) : ControllerBase
    {
        [Authorize]
        [HttpPost("creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> CreateGroupChat(Guid creatorId, [FromBody] GroupchatDTO request)
        {
            var result = await groupChatsServices.CreateGroupChatService(creatorId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroupChats()
        {
            var result = await groupChatsServices.GetAllGroupChatsService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("member/{userId:guid}")]
        public async Task<IActionResult> GetAllGroupChatsWhereUserIsMember(Guid userId)
        {
            var result = await groupChatsServices.GetAllGroupChatsForUser(userId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("created-by/{userId:guid}")]
        public async Task<IActionResult> GetAllGroupChatsCreatedByUser(Guid userId)
        {
            var result = await groupChatsServices.GetAllGroupChatsCreatedByUser(userId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> GetGroupChatById(Guid id)
        {
            var result = await groupChatsServices.GetGroupChatByIdService(id);
            return Ok(result);
        }

        [HttpGet("by-name")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> GetGroupChatByChatName([FromQuery] string chatName)
        {
            var result = await groupChatsServices.GetGroupChatByChatNameService(chatName);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("{id:guid}/members/{userId:guid}/creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> AddMemberToGroupChat(Guid creatorId, Guid userId, Guid id)
        {
            var result = await groupChatsServices.AddUserToGroupChatService(creatorId, userId, id);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/title/creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> EditGroupChatTitle(Guid creatorId, Guid id, [FromBody] GroupchatDTO request)
        {
            var result = await groupChatsServices.EditGroupChatTitleService(creatorId, id, request);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/members/{userId:guid}/creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> RemoveUserFromGroupChat(Guid creatorId, Guid userId, Guid id)
        {
            var result = await groupChatsServices.RemoveUserFromGroupChatService(creatorId, userId, id);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/creator/{creatorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Groupchat))]
        public async Task<IActionResult> DeleteGroupChat(Guid creatorId, Guid id)
        {
            var result = await groupChatsServices.DeleteGroupChatService(creatorId, id);
            return Ok(result);
        }
    }
}