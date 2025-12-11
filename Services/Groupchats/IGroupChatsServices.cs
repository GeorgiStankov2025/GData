using GData.DTOs.GroupchatsDTO;
using GData.Entity;

namespace GData.Services.Groupchats
{
    public interface IGroupChatsServices
    {
        
        public Task<Groupchat> CreateGroupChatService(Guid creatorId,GroupchatDTO request);
        public Task<Groupchat> EditGroupChatTitleService(Guid creatorId,Guid Id,GroupchatDTO request);
        public Task<List<Groupchat>> GetAllGroupChatsService();
        public Task<List<Groupchat>> GetAllGroupChatsForUser(Guid userId);
        public Task<List<Groupchat>> GetAllGroupChatsCreatedByUser(Guid userId);
        public Task<Groupchat> GetGroupChatByIdService(Guid Id);
        public Task<Groupchat> GetGroupChatByChatNameService(string title);
        public Task<Groupchat> DeleteGroupChatService(Guid creatorId, Guid id);
        public Task<Groupchat> AddUserToGroupChatService(Guid creatorId, Guid userId,Guid Id);
        public Task<Groupchat> RemoveUserFromGroupChatService(Guid creatorId, Guid userId,Guid Id);

    }
}
