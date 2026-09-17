using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.GroupchatsMessages;
using GData.Services.Groupchats;
using GData.Services.Users;

namespace GData.Services.GroupchatsMessages;

public class GroupchatsMessagesServices(
    IGroupChatsMessagesRepository groupChatsMessagesRepository,
    IAuthServices authServices,
    IGroupChatsServices groupChatsServices) : IGroupchatsMessagesServices
{
    public async Task<GroupchatMessage> CreateMessageService(Guid authorId, Guid groupChatId, GroupchatMessageDTO request)
    {
        var author = await authServices.GetUserByIdService(authorId);
        var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

        if (author is null)
        {
            throw new NotFoundException("Message author does not exist.");
        }

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        if (groupChat.ChatMembers.Contains(author) is false)
        {
            throw new ForbiddenException("Author is not a member of this group chat.");
        }

        if (author.IsEmailConfirmed is false)
        {
            throw new ForbiddenException("Author email is not verified.");
        }

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            throw new BadRequestException("No content has been provided for the message.");
        }

        var message = new GroupchatMessage()
        {
            MessageContent = request.Content,
            AuthorId = authorId,
            GroupchatId = groupChatId,
            DateCreated = DateTime.UtcNow,
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
            throw new NotFoundException("Message not found.");
        }

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat not found.");
        }

        if (author is null)
        {
            throw new NotFoundException("Message author not found.");
        }

        if (groupChat.ChatMembers.Contains(author) is false)
        {
            throw new ForbiddenException("Invalid author.");
        }

        if (groupChat.GroupchatMessages.Contains(message) is false)
        {
            throw new BadRequestException("Group chat is not valid for this message.");
        }

        if (message.AuthorId != authorId)
        {
            throw new ForbiddenException("Author is not valid.");
        }

        if (message.Author.IsEmailConfirmed is false)
        {
            throw new ForbiddenException("Author email is not verified.");
        }

        await groupChatsMessagesRepository.DeleteMessage(message);
        return message;
    }

    public async Task<GroupchatMessage> EditMessageService(Guid authorId, Guid groupChatId, Guid Id, GroupchatMessageDTO request)
    {
        var message = await groupChatsMessagesRepository.GetMessageById(Id);
        var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);
        var author = await authServices.GetUserByIdService(authorId);

        if (message is null)
        {
            throw new NotFoundException("Message not found.");
        }

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat not found.");
        }

        if (author is null)
        {
            throw new NotFoundException("Message author not found.");
        }

        if (groupChat.ChatMembers.Contains(author) is false)
        {
            throw new ForbiddenException("Invalid author.");
        }

        if (groupChat.GroupchatMessages.Contains(message) is false)
        {
            throw new BadRequestException("Group chat is not valid for this message.");
        }

        if (message.AuthorId != authorId)
        {
            throw new ForbiddenException("Author is not valid.");
        }

        if (message.Author.IsEmailConfirmed is false)
        {
            throw new ForbiddenException("Author email is not verified.");
        }

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            throw new BadRequestException("No content has been provided for the message.");
        }

        await groupChatsMessagesRepository.EditMessage(message, request);
        return message;
    }

    public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatService(Guid memberId, Guid groupChatId)
    {
        var member = await authServices.GetUserByIdService(memberId);
        var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

        List<GroupchatMessage> selectedMessages = new List<GroupchatMessage>();
        var messages = await groupChatsMessagesRepository.GetAllMessages();

        if (groupChat.ChatMembers.Contains(member) is false)
        {
            throw new ForbiddenException("Unauthorized user for this group chat.");
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

    public async Task<List<GroupchatMessage>> GetAllMessagesInGroupChatByUserService(Guid memberId, Guid userId, Guid groupChatId)
    {
        var member = await authServices.GetUserByIdService(memberId);
        var groupChat = await groupChatsServices.GetGroupChatByIdService(groupChatId);

        List<GroupchatMessage> selectedMessages = new List<GroupchatMessage>();
        var messages = await groupChatsMessagesRepository.GetAllMessages();

        if (groupChat.ChatMembers.Contains(member) is false)
        {
            throw new ForbiddenException("Unauthorized user for this group chat.");
        }

        foreach (var message in messages)
        {
            if (message.AuthorId == userId && message.GroupchatId == groupChatId)
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
        var message = await groupChatsMessagesRepository.GetMessageById(Id);

        if (message is null)
        {
            throw new NotFoundException("Message not found.");
        }

        return message;
    }
}