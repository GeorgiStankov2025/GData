using GData.DTOs.GroupchatsDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.GroupChat;
using GData.Services.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GData.Services.Groupchats
{
    public class GroupChatsServices(IGroupChatRepository groupChatRepository, IAuthServices authServices, GroupChatExceptionList groupChatExceptionList, UserExceptionList userExceptionList) : IGroupChatsServices
    {
        public async Task<Groupchat> AddUserToGroupChatService(Guid creatorId, Guid userId, Guid Id)
        {
            
            var user= await authServices.GetUserByIdService(userId);
            var groupChat = await GetGroupChatByIdService(Id);
            var creator = await authServices.GetUserByIdService(creatorId);

            if (groupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (groupChat.CreatorId != creatorId)
            {

                return await groupChatExceptionList.CreatorNotValid();

            }

            if (creator is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (creator.IsEmailConfirmed == false)
            {

                return await groupChatExceptionList.UnverifiedUserEmail();

            }

            foreach (var member in groupChat.ChatMembers)
            {

                if (member.Id == userId)
                {

                    return await groupChatExceptionList.AddUserAlreadyAdded();

                }
            }

            await groupChatRepository.AddUserToGroupChat(user, groupChat);
            return groupChat;

        }

        public async Task<Groupchat> CreateGroupChatService(Guid creatorId, GroupchatDTO request)
        {

            var creator=await authServices.GetUserByIdService(creatorId);

            if (creator is null)
            {

                return await groupChatExceptionList.ChatCreatorDoesNotExist();

            }

            if (creator.IsEmailConfirmed == false)
            {

                return await groupChatExceptionList.UnverifiedUserEmail();

            }

            if (string.IsNullOrWhiteSpace(request.ChatTitle))
            {

                return await groupChatExceptionList.ChatNameNeedsToHaveData();

            }

            if(request.ChatTitle.Length<3)
            {

                return await groupChatExceptionList.ChatNameNeedsToHaveMoreThanThreeChars();

            }

            var groupchat = new Groupchat()
            {

                ChatName = request.ChatTitle,
                CreatorId= creatorId,
                DateCreated= DateTime.UtcNow,

            };
            await groupChatRepository.CreateGroupChat(groupchat);

            var createdGroupChat=await GetGroupChatByChatNameService(request.ChatTitle);

            if(createdGroupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            await AddUserToGroupChatService(creatorId,creatorId, createdGroupChat.Id);
            return groupchat;

        }

        public async Task<Groupchat> DeleteGroupChatService(Guid creatorId, Guid Id)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatById(Id);

            var creator = await authServices.GetUserByIdService(creatorId);

            if (groupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (groupChat.CreatorId != creatorId)
            {

                return await groupChatExceptionList.CreatorNotValid();

            }

            if (creator is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (creator.IsEmailConfirmed == false)
            {

                return await groupChatExceptionList.UnverifiedUserEmail();

            }

            await groupChatRepository.DeleteGroupChat(groupChat);
            return groupChat;

        }

        public async Task<Groupchat> EditGroupChatTitleService(Guid creatorId, Guid Id, GroupchatDTO request)
        {

            var groupChat = await groupChatRepository.GetGroupchatById(Id);

            var creator = await authServices.GetUserByIdService(creatorId);

            if(groupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }
            
            if(groupChat.CreatorId != creatorId)
            {

                return await groupChatExceptionList.CreatorNotValid();

            }
            
            if(creator is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }
            
            if (creator.IsEmailConfirmed == false)
            {

                return await groupChatExceptionList.UnverifiedUserEmail();

            }
            
            if (string.IsNullOrWhiteSpace(request.ChatTitle))
            {

                return await groupChatExceptionList.ChatNameNeedsToHaveData();

            }

            if(request.ChatTitle.Length<3)
            {

                return await groupChatExceptionList.ChatNameNeedsToHaveMoreThanThreeChars();

            }

            await groupChatRepository.EditGroupChatTitle(groupChat,request);
            return groupChat;

        }

        public async Task<List<Groupchat>> GetAllGroupChatsService()
        {
            
            return await groupChatRepository.GetAllGroupchats();

        }

        public async Task<Groupchat> GetGroupChatByIdService(Guid Id)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatById(Id);

            if(groupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            return groupChat;

        }

        public async Task<Groupchat> GetGroupChatByChatNameService(string title)
        {
            
            var groupChat=await groupChatRepository.GetGroupchatByChatName(title);

            if (groupChat is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

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

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (groupChat.CreatorId != creatorId)
            {

                return await groupChatExceptionList.CreatorNotValid();

            }

            if (creator is null)
            {

                return await groupChatExceptionList.EditGroupChatChatDoesNotExist();

            }

            if (creator.IsEmailConfirmed == false)
            {

                return await groupChatExceptionList.UnverifiedUserEmail();

            }

            if(user==creator)
            {

                return await groupChatExceptionList.RemoveUserIsOwner();

            }

            if(user is null)
            {

                return await groupChatExceptionList.UserAlreadyRemoved();

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

                if (groupchat.CreatorId==userId)
                {

                    selectedGroupChats.Add(groupchat);

                }

            }

            return selectedGroupChats;
        }
    }
}
