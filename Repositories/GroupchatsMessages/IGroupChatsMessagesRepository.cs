using GData.DTOs.GroupchatsDTO;
using GData.Entity;

namespace GData.Repositories.GroupchatsMessages
{
    public interface IGroupChatsMessagesRepository
    {

        public Task<GroupchatMessage> CreateMessage(GroupchatMessage message);
        public Task<GroupchatMessage> EditMessage(GroupchatMessage message,GroupchatMessageDTO request);
        public Task<List<GroupchatMessage>> GetAllMessages();
        public Task<GroupchatMessage> GetMessageById(Guid Id);
        public Task<GroupchatMessage> DeleteMessage(GroupchatMessage message); 

    }
}
