using GData.Entity;

namespace GData.Exceptions
{
    public class PostsExceptionList
    {

        //CreatePost

        public Task<Post> CreatePostOwnerDoesNotExist()
        {

            throw new ArgumentNullException("The post cannot be created because its owner does not exist or was deleted!");

        }

        public Task<Post> NoTitleHasBeenProvidedForPost()
        {

            throw new FormatException("No data has been provided for the post title!");

        }

        public Task<Post> TitleNeedsToHaveMoreThanThreeChars()
        {

            throw new FormatException("The provided title needs to have three or more characters");

        }

        public Task<Post> UnverifiedOwner()
        {

            throw new UnauthorizedAccessException("Post owner's email address is not verified!");

        }

    }
}
