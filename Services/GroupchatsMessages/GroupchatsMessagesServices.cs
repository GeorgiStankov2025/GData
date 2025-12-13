using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Repositories.GroupchatsMessages;
using GData.Services.Groupchats;
using GData.Services.Users;

namespace GData.Services.GroupchatsMessages
{
    public class GroupchatsMessagesServices(IGroupChatsMessagesRepository groupChatsMessagesRepository, IAuthServices authServices, IGroupChatsServices groupChatsServices) : IGroupchatsMessagesServices
    {
        public async Task<GroupchatMessage> CreateMessageService(Guid authorId, Guid groupChatId, GroupchatMessageDTO request)
        {

            var author= await authServices.GetUserByIdService(authorId);

            var groupChat=await groupChatsServices.GetGroupChatByIdService(groupChatId);


            var message = new GroupchatMessage()
            {

                MessageContent=request.Content,
                AuthorId=authorId,
                GroupchatId=groupChatId,
                DateCreated=DateTime.UtcNow,

            };

            await groupChatsMessagesRepository.CreateMessage(message);
            return message;

        }

        public async Task<GroupchatMessage> DeleteMessageService(Guid authorId, Guid groupChatId, Guid Id)
        {

            var message = await groupChatsMessagesRepository.GetMessageById(Id);
            await groupChatsMessagesRepository.DeleteMessage(message);
            return message;

        }

        public async Task<GroupchatMessage> EditMessageService(Guid authorId, Guid groupChatId, Guid Id, GroupchatMessageDTO request)
        {

            var message = await groupChatsMessagesRepository.GetMessageById(Id);
            await groupChatsMessagesRepository.EditMessage(message, request);

            return message;

        }

        public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatService(Guid groupChatId)
        {

            List<GroupchatMessage> selectedMessages = new List<GroupchatMessage>();

            var messages = await groupChatsMessagesRepository.GetAllMessages();

            foreach (var message in messages)
            {

                if (message.GroupchatId == groupChatId)
                {

                    selectedMessages.Add(message);

                }

            }

            return selectedMessages;

        }

        public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatByUserService(Guid userId, Guid groupChatId)
        {

            List<GroupchatMessage> selectedMessages= new List<GroupchatMessage>();

            var messages= await groupChatsMessagesRepository.GetAllMessages();

            foreach(var message in messages)
            {

                if(message.AuthorId==userId&&message.GroupchatId==groupChatId)
                {

                    selectedMessages.Add(message);

                }

            }

            return selectedMessages;

        }

        public async Task<List<GroupchatMessage>> GetAllMessagesService()
        {
            
            return await groupChatsMessagesRepository.GetAllMessages();

        }

        public async Task<GroupchatMessage> GetMessageByIdService(Guid Id)
        {

            return await groupChatsMessagesRepository.GetMessageById(Id);

        }
    }
}
