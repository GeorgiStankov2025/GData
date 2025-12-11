using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Services.Groupchats;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupChatsController(IGroupChatsServices groupChatsServices) : ControllerBase
    {

        [HttpPost("create-GroupChat{creatorId}")]
        public async Task<ActionResult<Groupchat>> CreateGroupChat(Guid creatorId, GroupchatDTO request)
        {

            var result= await groupChatsServices.CreateGroupChatService(creatorId, request);
            return Ok(result);

        }

        [HttpGet("get-All-GroupChats")]
        public async Task<ActionResult<List<Groupchat>>> GetAllGroupChats()
        {

            var result= await groupChatsServices.GetAllGroupChatsService();
            return Ok(result);

        }

        [HttpGet("get-GroupChat-By-Id{Id}")]
        public async Task<ActionResult<Groupchat>> GetGroupChatById(Guid Id)
        {

            var result= await groupChatsServices.GetGroupChatByIdService(Id);
            return Ok(result);

        }

        [HttpGet("get-GroupChat-By-ChatName")]
        public async Task<ActionResult<Groupchat>> GetGroupChatByChatName(string chatName)
        {

            var result= await groupChatsServices.GetGroupChatByChatNameService(chatName);
            return Ok(result);

        }

        [HttpPatch("add-Chat-Member{creatorId},{userId},{Id}")]
        public async Task<ActionResult<Groupchat>> AddMemberToGroupChat(Guid creatorId,Guid userId,Guid Id)
        {

            var result=await groupChatsServices.AddUserToGroupChatService(creatorId, userId, Id);
            return Ok(result);

        }

        [HttpPatch("edit-GropuChat-Title{creatorId},{Id}")]
        public async Task<ActionResult<Groupchat>> EditGroupChatTitle(Guid creatorId,Guid Id,GroupchatDTO request)
        {

            var result=await groupChatsServices.EditGroupChatTitleService(creatorId,Id,request);
            return Ok(result);

        }

        [HttpPatch("remove-User-From-GroupChat{creatorId},{userId},{Id}")]
        public async Task<ActionResult<Groupchat>> RemoveUserFromGroupChat(Guid creatorId,Guid userId,Guid Id)
        {

            var result=await groupChatsServices.RemoveUserFromGroupChatService(creatorId,userId,Id);
            return Ok(result);

        }

        [HttpDelete("delete-GroupChat{creatorId},{Id}")]
        public async Task<ActionResult<Groupchat>> DeleteGroupChat(Guid creatorId,Guid Id)
        {

            var result = await groupChatsServices.DeleteGroupChatService(creatorId, Id);
            return Ok(result);
        
        }

    }
}
