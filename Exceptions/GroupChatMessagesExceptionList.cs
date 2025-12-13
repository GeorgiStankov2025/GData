using GData.Entity;

namespace GData.Exceptions
{
    public class GroupChatMessagesExceptionList
    {

        //CreateMessage

        public Task<GroupchatMessage> CreateMessageAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The message cannot be created because it's author does not exist or was deleted.");

        }

        public Task<GroupchatMessage> CreateMessageGroupChatDoesNotExist()
        {

            throw new ArgumentNullException("The message cannot be created because it's groupchat was deleted or does not exist.");

        }

        public Task<GroupchatMessage> NoContenthasBeenProvidedForMessages() 
        {

            throw new FormatException("No data has been provided for message");

        }

        public Task<GroupchatMessage> InvalidAuthor() //Also used in edit message
        {

            throw new UnauthorizedAccessException("Message author is not part of the group chat or was removed"); 

        }

        public Task<GroupchatMessage> UnverifiedAuthor()
        {

            throw new UnauthorizedAccessException("The author's email address is not confirmed.");

        }

        //GetMessagesForGroupChat

        public Task<GroupchatMessage> UnauthorizedUser()
        {

            throw new UnauthorizedAccessException("User is not part of the group chat or was removed");

        }

        //EditMessage/DeleteMessage

        public Task<GroupchatMessage> EditMessageNotFound()
        {

            throw new ArgumentNullException("Message cannot be deleted because it does not exist or was deleted.");

        }

        public Task<GroupchatMessage> EditMessageGropChatNotFound()
        {

            throw new ArgumentNullException("The message cannot be edited because it does not exist or was deleted.");

        }

        public Task<GroupchatMessage> AuthorNotValid()
        {

            throw new UnauthorizedAccessException("The user who is trying to edit the message is not it's author!"); //Also used in delete message

        }

        public Task<GroupchatMessage> GroupChatNotValid()
        {

            throw new UnauthorizedAccessException("The edited message does not belong to this group chat"); //Also used in delete message

        }

    }
}
