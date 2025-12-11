using GData.DTOs.GroupchatsDTO;
using GData.Entity;

namespace GData.Repositories.GroupChat
{
    public interface IGroupChatRepository
    {

        public Task<Groupchat> CreateGroupChat(Groupchat groupchat);
        public Task<List<Groupchat>> GetAllGroupchats();
        public Task<Groupchat> GetGroupchatById(Guid Id);
        public Task<Groupchat> GetGroupchatByChatName(string chatName);
        public Task<Groupchat> EditGroupChatTitle(Groupchat groupchat, GroupchatDTO request);
        public Task<Groupchat> DeleteGroupChat(Groupchat groupchat);
        public Task<Groupchat> AddUserToGroupChat(User user, Groupchat groupchat);
        public Task<Groupchat> RemoveUserFromGroupChat(User user, Groupchat groupchat);

    }
}
