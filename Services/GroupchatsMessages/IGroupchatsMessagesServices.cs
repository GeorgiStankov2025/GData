using GData.DTOs.GroupchatsDTO;
using GData.Entity;

namespace GData.Services.GroupchatsMessages
{
    public interface IGroupchatsMessagesServices
    {

        public Task<GroupchatMessage> CreateMessageService(Guid authorId, Guid groupChatId,GroupchatMessageDTO request);
        public Task<GroupchatMessage> EditMessageService(Guid authorId, Guid groupChatId, Guid Id, GroupchatMessageDTO request);
        public Task<GroupchatMessage> DeleteMessageService(Guid authorId, Guid groupChatId, Guid Id);
        public Task<GroupchatMessage> GetMessageByIdService(Guid Id);
        public Task<List<GroupchatMessage>> GetAllMessagesService();
        public Task<List<GroupchatMessage>> GetAllMessagesInGroupChatService(Guid memberId,Guid groupChatId);
        public Task<List<GroupchatMessage>> GetAllMessagesInGroupChatByUserService(Guid memberId, Guid userId,Guid groupChatId);

    }
}
