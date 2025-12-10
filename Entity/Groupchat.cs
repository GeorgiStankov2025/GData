namespace GData.Entity
{
    public class Groupchat
    {
        public Guid Id { get; set; }
        public required string ChatName { get; set; }=string.Empty;
        
        public Guid CreatorId { get; set; }
        public User? ChatCreator { get; set; }

        public List<User>? ChatMembers { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
