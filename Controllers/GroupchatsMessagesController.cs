using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.GroupchatsMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupchatsMessagesController(IGroupchatsMessagesServices groupchatsMessagesServices) : ControllerBase
    {

        [HttpPost("create-Message{authorId},{groupChatId}")]
        public async Task<ActionResult<GroupchatMessage>> CreateMessage(Guid authorId, Guid groupChatId, GroupchatMessageDTO request)
        {

            var result= await groupchatsMessagesServices.CreateMessageService(authorId, groupChatId, request);
            return Ok(result);

        }

        [HttpGet("get-All-Messages")]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessages()
        {

            var result= await groupchatsMessagesServices.GetAllMessagesService();
            return Ok(result);

        }

        [HttpGet("get-All-Messages-In-Group-Chat{groupChatId}")]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessagesInGroupChat(Guid groupChatId)
        {

            var result= await groupchatsMessagesServices.GetAllMessagesInGroupChatService(groupChatId);
            return Ok(result);

        }

        [HttpGet("get-All-Messages-By-User-In-GroupChat{groupChatId},{userId}")]
        public async Task<ActionResult<List<GroupchatMessage>>> GetAllMessagesInGroupChatByUser(Guid groupChatId,Guid userId)
        {

            var result= await groupchatsMessagesServices.GetAllMessagesInGroupChatByUserService(userId,groupChatId);
            return Ok(result);

        }

        [HttpPatch("edit-Message{authorId},{groupChatId},{Id}")]
        public async Task<ActionResult<GroupchatMessage>> EditMessage(Guid authorId,Guid groupChatId, Guid Id, GroupchatMessageDTO request)
        {

            var result = await groupchatsMessagesServices.EditMessageService(authorId, groupChatId, Id, request);
            return Ok(result);

        }

        [HttpDelete("delete-Message{authorId},{groupChatId},{Id}")]
        public async Task<ActionResult<GroupchatMessage>> DeleteMessage(Guid authorId, Guid groupChatId, Guid Id)
        {

            var result = await groupchatsMessagesServices.DeleteMessageService(authorId, groupChatId, Id);
            return Ok(result);

        }

    }
}
