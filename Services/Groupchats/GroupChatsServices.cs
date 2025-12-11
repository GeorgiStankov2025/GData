using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Repositories.GroupChat;
using GData.Services.Users;

namespace GData.Services.Groupchats
{
    public class GroupChatsServices(IGroupChatRepository groupChatRepository, IAuthServices authServices) : IGroupChatsServices
    {
        public async Task<Groupchat> AddUserToGroupChatService(Guid creatorId, Guid userId, Guid Id)
        {
            
           var user= await authServices.GetUserByIdService(userId);
           var groupChat = await GetGroupChatByIdService(Id);
            
           await groupChatRepository.AddUserToGroupChat(user, groupChat);
           return groupChat;

        }

        public async Task<Groupchat> CreateGroupChatService(Guid creatorId, GroupchatDTO request)
        {

            var creator=await authServices.GetUserByIdService(creatorId);

            var groupchat = new Groupchat()
            {

                ChatName = request.ChatTitle,
                CreatorId= creatorId,
                DateCreated= DateTime.UtcNow,

            };
            await groupChatRepository.CreateGroupChat(groupchat);

            var createdGroupChat=await GetGroupChatByChatNameService(request.ChatTitle);
            await AddUserToGroupChatService(creatorId,creatorId, createdGroupChat.Id);
            return groupchat;

        }

        public async Task<Groupchat> DeleteGroupChatService(Guid creatorId, Guid id)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatById(id);

            await groupChatRepository.DeleteGroupChat(groupChat);
            return groupChat;

        }

        public async Task<Groupchat> EditGroupChatTitleService(Guid creatorId, Guid Id, GroupchatDTO request)
        {

            var groupChat = await groupChatRepository.GetGroupchatById(Id);

            await groupChatRepository.EditGroupChatTitle(groupChat,request);
            return groupChat;

        }

        public async Task<List<Groupchat>> GetAllGroupChatsService()
        {
            
            return await groupChatRepository.GetAllGroupchats();

        }

        public async Task<Groupchat> GetGroupChatByIdService(Guid Id)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatById(Id);
            return groupChat;

        }

        public async Task<Groupchat> GetGroupChatByChatNameService(string title)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatByChatName(title);
            return groupChat;

        }

        public async Task<Groupchat> RemoveUserFromGroupChatService(Guid creatorId, Guid userId, Guid Id)
        {

            var user = await authServices.GetUserByIdService(userId);
            var groupChat = await GetGroupChatByIdService(Id);

            await groupChatRepository.RemoveUserFromGroupChat(user, groupChat);
            return groupChat;

        }
    }
}
