using GData.Data;
using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;

namespace GData.Repositories.GroupchatsMessages
{
    public class GroupChatsMessagesRepository(GDataDbContext dbContext) : IGroupChatsMessagesRepository
    {
        public async Task<GroupchatMessage> CreateMessage(GroupchatMessage message)
        {
            
            await dbContext.AddAsync(message);
            await dbContext.SaveChangesAsync();
            return message;

        }

        public async Task<GroupchatMessage> DeleteMessage(GroupchatMessage message)
        {
            
            dbContext.Remove(message);
            await dbContext.SaveChangesAsync();
            return message;

        }

        public async Task<GroupchatMessage> EditMessage(GroupchatMessage message, GroupchatMessageDTO request)
        {
            
            message.MessageContent=request.Content;
            await dbContext.SaveChangesAsync();
            return message;

        }

        public async Task<List<GroupchatMessage>> GetAllMessages()
        {

            return await dbContext.GroupchatMessages.Include<GroupchatMessage,User>(gcm=>gcm.Author).Include<GroupchatMessage,Groupchat>(gcm=>gcm.Groupchat).ToListAsync();
        
        }

        public async Task<GroupchatMessage> GetMessageById(Guid Id)
        {

            return await dbContext.GroupchatMessages.Include<GroupchatMessage, User>(gcm => gcm.Author).Include<GroupchatMessage, Groupchat>(gcm => gcm.Groupchat).FirstOrDefaultAsync(gcm => gcm.Id == Id);

        }
    }
}
