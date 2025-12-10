using GData.Data;
using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Repositories.GroupChat;

namespace GData.Repositories.Groupchats
{
    public class GroupChatRepository(GDataDbContext dbContext) : IGroupChatRepository
    {
        public async Task<Groupchat> CreateGroupChat(Groupchat groupchat)
        {
            
            await dbContext.AddAsync(groupchat);
            await dbContext.SaveChangesAsync();
            return groupchat;

        }

        public Task<Groupchat> DeleteGroupChat(Groupchat groupchat)
        {
            throw new NotImplementedException();
        }

        public Task<Groupchat> EditGroupChatTitle(Groupchat groupchat, GroupchatDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<List<Groupchat>> GetAllGroupchats()
        {
            throw new NotImplementedException();
        }

        public Task<Groupchat> GetGroupchatByChatName(string chatName)
        {
            throw new NotImplementedException();
        }

        public Task<Groupchat> GetGroupchatById(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
