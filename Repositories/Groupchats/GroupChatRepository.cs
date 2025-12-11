using GData.Data;
using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Repositories.GroupChat;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Groupchat> DeleteGroupChat(Groupchat groupchat)
        {
            
            dbContext.Groupchats.Remove(groupchat);
            await dbContext.SaveChangesAsync();
            return groupchat;

        }

        public async Task<Groupchat> EditGroupChatTitle(Groupchat groupchat, GroupchatDTO request)
        {
            
            groupchat.ChatName=request.ChatTitle;
            await dbContext.SaveChangesAsync();
            return groupchat;

        }

        public async Task<List<Groupchat>> GetAllGroupchats()
        {

            return await dbContext.Groupchats.Include<Groupchat,List<User>>(gc=>gc.ChatMembers).ToListAsync();

        }

        public async Task<Groupchat> GetGroupchatByChatName(string chatName)
        {
            return await dbContext.Groupchats.Include<Groupchat, List<User>>(gc => gc.ChatMembers).FirstOrDefaultAsync(gc=>gc.ChatName==chatName);
        }

        public async Task<Groupchat> GetGroupchatById(Guid Id)
        {
            return await dbContext.Groupchats.Include<Groupchat, List<User>>(gc => gc.ChatMembers).FirstOrDefaultAsync(gc => gc.Id==Id);
        }
    }
}
