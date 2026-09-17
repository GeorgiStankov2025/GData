using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Repositories.GroupChat;
using GData.Services.Users;
using GData.Exceptions;

namespace GData.Services.Groupchats;

public class GroupChatsServices(
    IGroupChatRepository groupChatRepository,
    IAuthServices authServices) : IGroupChatsServices
{
    public async Task<Groupchat> AddUserToGroupChatService(Guid creatorId, Guid userId, Guid Id)
    {
        var user = await authServices.GetUserByIdService(userId);
        var groupChat = await GetGroupChatByIdService(Id);
        var creator = await authServices.GetUserByIdService(creatorId);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        if (groupChat.CreatorId != creatorId)
        {
            throw new ForbiddenException("Invalid creator ID.");
        }

        if (creator is null)
        {
            throw new NotFoundException("Creator does not exist.");
        }

        if (creator.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Creator email is not verified.");
        }

        foreach (var member in groupChat.ChatMembers)
        {
            if (member.Id == userId)
            {
                throw new BadRequestException("User is already in this group chat.");
            }
        }

        await groupChatRepository.AddUserToGroupChat(user, groupChat);
        return groupChat;
    }

    public async Task<Groupchat> CreateGroupChatService(Guid creatorId, GroupchatDTO request)
    {
        var creator = await authServices.GetUserByIdService(creatorId);

        if (creator is null)
        {
            throw new NotFoundException("Chat creator does not exist.");
        }

        if (creator.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Creator email is not verified.");
        }

        if (string.IsNullOrWhiteSpace(request.ChatTitle))
        {
            throw new BadRequestException("Chat title is required.");
        }

        if (request.ChatTitle.Length < 3)
        {
            throw new BadRequestException("Chat title needs to be at least 3 characters.");
        }

        var groupchat = new Groupchat()
        {
            ChatName = request.ChatTitle,
            CreatorId = creatorId,
            DateCreated = DateTime.UtcNow,
        };

        await groupChatRepository.CreateGroupChat(groupchat);

        var createdGroupChat = await GetGroupChatByChatNameService(request.ChatTitle);

        if (createdGroupChat is null)
        {
            throw new NotFoundException("Group chat could not be retrieved after creation.");
        }

        await AddUserToGroupChatService(creatorId, creatorId, createdGroupChat.Id);
        return groupchat;
    }

    public async Task<Groupchat> DeleteGroupChatService(Guid creatorId, Guid Id)
    {
        var groupChat = await groupChatRepository.GetGroupchatById(Id);
        var creator = await authServices.GetUserByIdService(creatorId);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        if (groupChat.CreatorId != creatorId)
        {
            throw new ForbiddenException("Invalid creator ID.");
        }

        if (creator is null)
        {
            throw new NotFoundException("Creator does not exist.");
        }

        if (creator.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Creator email is not verified.");
        }

        await groupChatRepository.DeleteGroupChat(groupChat);
        return groupChat;
    }

    public async Task<Groupchat> EditGroupChatTitleService(Guid creatorId, Guid Id, GroupchatDTO request)
    {
        var groupChat = await groupChatRepository.GetGroupchatById(Id);
        var creator = await authServices.GetUserByIdService(creatorId);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        if (groupChat.CreatorId != creatorId)
        {
            throw new ForbiddenException("Invalid creator ID.");
        }

        if (creator is null)
        {
            throw new NotFoundException("Creator does not exist.");
        }

        if (creator.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Creator email is not verified.");
        }

        if (string.IsNullOrWhiteSpace(request.ChatTitle))
        {
            throw new BadRequestException("Chat title is required.");
        }

        if (request.ChatTitle.Length < 3)
        {
            throw new BadRequestException("Chat title needs to be at least 3 characters.");
        }

        await groupChatRepository.EditGroupChatTitle(groupChat, request);
        return groupChat;
    }

    public async Task<List<Groupchat>> GetAllGroupChatsService()
    {
        return await groupChatRepository.GetAllGroupchats();
    }

    public async Task<Groupchat> GetGroupChatByIdService(Guid Id)
    {
        var groupChat = await groupChatRepository.GetGroupchatById(Id);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        return groupChat;
    }

    public async Task<Groupchat> GetGroupChatByChatNameService(string title)
    {
        var groupChat = await groupChatRepository.GetGroupchatByChatName(title);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        return groupChat;
    }

    public async Task<Groupchat> RemoveUserFromGroupChatService(Guid creatorId, Guid userId, Guid Id)
    {
        var creator = await authServices.GetUserByIdService(creatorId);
        var user = await authServices.GetUserByIdService(userId);
        var groupChat = await GetGroupChatByIdService(Id);

        if (groupChat is null)
        {
            throw new NotFoundException("Group chat does not exist.");
        }

        if (groupChat.CreatorId != creatorId)
        {
            throw new ForbiddenException("Invalid creator ID.");
        }

        if (creator is null)
        {
            throw new NotFoundException("Creator does not exist.");
        }

        if (creator.IsEmailConfirmed == false)
        {
            throw new ForbiddenException("Creator email is not verified.");
        }

        if (user == creator)
        {
            throw new BadRequestException("Owner cannot be removed from group chat.");
        }

        if (user is null)
        {
            throw new NotFoundException("User to remove does not exist.");
        }

        await groupChatRepository.RemoveUserFromGroupChat(user, groupChat);
        return groupChat;
    }

    public async Task<List<Groupchat>> GetAllGroupChatsForUser(Guid userId)
    {
        List<Groupchat> selectedGroupChats = new List<Groupchat>();
        var groupchats = await GetAllGroupChatsService();
        var user = await authServices.GetUserByIdService(userId);

        foreach (var groupchat in groupchats)
        {
            if (groupchat.ChatMembers.Contains(user))
            {
                selectedGroupChats.Add(groupchat);
            }
        }

        return selectedGroupChats;
    }

    public async Task<List<Groupchat>> GetAllGroupChatsCreatedByUser(Guid userId)
    {
        List<Groupchat> selectedGroupChats = new List<Groupchat>();
        var groupchats = await GetAllGroupChatsService();
        var user = await authServices.GetUserByIdService(userId);

        foreach (var groupchat in groupchats)
        {
            if (groupchat.CreatorId == userId)
            {
                selectedGroupChats.Add(groupchat);
            }
        }

        return selectedGroupChats;
    }
}
