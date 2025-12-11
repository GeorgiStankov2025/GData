using GData.Entity;

namespace GData.Exceptions
{
    public class GroupChatExceptionList
    {

        //Create GroupChat

        public Task<Groupchat> ChatCreatorDoesNotExist()
        {

            throw new ArgumentNullException("The groupchat cannot be created because its creator was deleted or does not exist."); 

        }

        public Task<Groupchat> ChatNameNeedsToHaveData()
        {

            throw new ArgumentNullException("The groupchat cannot be created because no data was provided for chat name."); //Also used in edit groupchat title

        }

        public Task<Groupchat> ChatNameNeedsToHaveMoreThanThreeChars()
        {

            throw new FormatException("Chat name needs to have more than three characters."); //Also used in edit groupchat title

        }
        public Task<Groupchat> UnverifiedUserEmail() //Alo used in edit group chat title
        {

            throw new UnauthorizedAccessException("The group chat cannot be created because it's creator's email is not verified.");

        }
         
        //Edit GroupChatTitle

        public Task<Groupchat> EditGroupChatChatDoesNotExist()
        {

            throw new ArgumentNullException("The group chat cannot be edited because it was deleted or does not exist.");

        }

        public Task<Groupchat> EditChatCreatorDoesNotExist()
        {

            throw new ArgumentNullException("The groupchat cannot be edited because its creator was deleted or does not exist.");

        }

        public Task<Groupchat> CreatorNotValid()
        {

            throw new UnauthorizedAccessException("The user who is trying to edit the group chat is not it's owner!");

        }

        //Add/Remove User to groupchat exceptions
        
        public Task<Groupchat> RemoveUserIsOwner()
        {

            throw new FormatException("The user cannot be removed because he is the chat's owner");

        }

        public Task<Groupchat> UserAlreadyRemoved()
        {

            throw new ArgumentNullException("The user is already removed or never existed");

        }

        public Task<Groupchat> AddUserAlreadyAdded()
        {

            throw new FormatException("The user is already added to the groupchat.");

        }

    }
}
