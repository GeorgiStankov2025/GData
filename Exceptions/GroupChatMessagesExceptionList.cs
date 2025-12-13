using GData.Entity;

namespace GData.Exceptions
{
    public class GroupChatMessagesExceptionList
    {
        public Task<GroupchatMessage>  CreateMessageAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The message cannot be created because it's author does not exist or was deleted.");

        }

        public Task<GroupchatMessage> CreateMessageGroupChatDoesNotExist()
        {

            throw new ArgumentNullException("The message cannot be created because it's groupchat was deleted or does not exist.");

        }

    }
}
