using GData.Entity;

namespace GData.Exceptions
{
    public class GroupChatExceptionList
    {

        public Task<Groupchat> ChatCreatorDoesNotExist()
        {

            throw new ArgumentNullException("The groupchat cannot be created because its creator was deleted or does not exist.");

        }

        public Task<Groupchat> ChatNameNeedsToHaveData()
        {

            throw new ArgumentNullException("The groupchat cannot be created because no data was provided for chat name.");

        }

    }
}
