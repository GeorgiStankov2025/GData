using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.GroupchatsMessages;
using GData.Services.Groupchats;
using GData.Services.Users;

namespace GData.Services.GroupchatsMessages
{
    public class GroupchatsMessagesServices(IGroupChatsMessagesRepository groupChatsMessagesRepository, IAuthServices authServices, IGroupChatsServices groupChatsServices,  GroupChatMessagesExceptionList groupChatMessagesExceptionList) : IGroupchatsMessagesServices
    {
        public async Task<GroupchatMessage> CreateMessageService(Guid authorId, Guid groupChatId, GroupchatMessageDTO request)
        {

            var author= await authServices.GetUserByIdService(authorId);

            var groupChat=await groupChatsServices.GetGroupChatByIdService(groupChatId);

            if(author is null)
            {

                return await groupChatMessagesExceptionList.CreateMessageAuthorDoesNotExist();

            }

            if(groupChat is null)
            {

                return await groupChatMessagesExceptionList.CreateMessageGroupChatDoesNotExist();

            }

            if(groupChat.ChatMembers.Contains(author) is false)
            {

                return await groupChatMessagesExceptionList.InvalidAuthor();

            }

            if(author.IsEmailConfirmed is false)
            {

                return await groupChatMessagesExceptionList.UnverifiedAuthor();

            }

            if(string.IsNullOrWhiteSpace(request.Content))
            {

                return await groupChatMessagesExceptionList.NoContenthasBeenProvidedForMessages();  

            }
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

            var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

            var author = await authServices.GetUserByIdService(authorId);

            if (message is null)
            {

                return await groupChatMessagesExceptionList.EditMessageNotFound();

            }

            if (groupChat is null)
            {

                return await groupChatMessagesExceptionList.EditMessageGropChatNotFound();

            }

            if (author is null)
            {

                return await groupChatMessagesExceptionList.EditMessageAuthorNotFound();

            }

            if (groupChat.ChatMembers.Contains(author) is false)
            {

                return await groupChatMessagesExceptionList.InvalidAuthor();

            }

            if (groupChat.GroupchatMessages.Contains(message) is false)
            {

                return await groupChatMessagesExceptionList.GroupChatNotValid();

            }

            if (message.AuthorId != authorId)
            {

                return await groupChatMessagesExceptionList.AuthorNotValid();

            }

            if (message.Author.IsEmailConfirmed is false)
            {

                return await groupChatMessagesExceptionList.UnverifiedAuthor();

            }

            await groupChatsMessagesRepository.DeleteMessage(message);
            return message;

        }

        public async Task<GroupchatMessage> EditMessageService(Guid authorId, Guid groupChatId, Guid Id, GroupchatMessageDTO request)
        {

            var message = await groupChatsMessagesRepository.GetMessageById(Id);

            var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

            var author= await authServices.GetUserByIdService(authorId);

            if (message is null)
            {

                return await groupChatMessagesExceptionList.EditMessageNotFound();

            }

            if(groupChat is null)
            {

                return await groupChatMessagesExceptionList.EditMessageGropChatNotFound();

            }

            if(author is null)
            {

                return await groupChatMessagesExceptionList.EditMessageAuthorNotFound();

            }

            if(groupChat.ChatMembers.Contains(author) is false)
            {

                return await groupChatMessagesExceptionList.InvalidAuthor();

            }

            if(groupChat.GroupchatMessages.Contains(message) is false)
            {

                return await groupChatMessagesExceptionList.GroupChatNotValid();

            }

            if(message.AuthorId!=authorId)
            {

                return await groupChatMessagesExceptionList.AuthorNotValid();

            }

            if (message.Author.IsEmailConfirmed is false)
            {

                return await groupChatMessagesExceptionList.UnverifiedAuthor();

            }

            if(string.IsNullOrWhiteSpace(request.Content))
            {

                return await groupChatMessagesExceptionList.NoContenthasBeenProvidedForMessages();

            }

            await groupChatsMessagesRepository.EditMessage(message, request);

            return message;

        }

        public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatService(Guid memberId,Guid groupChatId)
        {

            var member=await authServices.GetUserByIdService(memberId);

            var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

            List<GroupchatMessage> selectedMessages = new List<GroupchatMessage>();

            var messages = await groupChatsMessagesRepository.GetAllMessages();

            if (groupChat.ChatMembers.Contains(member) is false)
            {

                return await groupChatMessagesExceptionList.UnauthorizedUser();

            }

            foreach (var message in messages)
            {

                if (message.GroupchatId == groupChatId)
                {

                    selectedMessages.Add(message);

                }

            }

            return selectedMessages;

        }

        public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatByUserService(Guid memberId,Guid userId, Guid groupChatId)
        {

            var member = await authServices.GetUserByIdService(memberId);

            var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

            List<GroupchatMessage> selectedMessages= new List<GroupchatMessage>();

            var messages= await groupChatsMessagesRepository.GetAllMessages();

            if (groupChat.ChatMembers.Contains(member) is false)
            {

                return await groupChatMessagesExceptionList.UnauthorizedUser();

            }

            foreach (var message in messages)
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

            var message= await groupChatsMessagesRepository.GetMessageById(Id);

            if(message is null)
            {

                return await groupChatMessagesExceptionList.EditMessageNotFound();

            }

            return message;

        }
    }
}
