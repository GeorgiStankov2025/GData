using GData.Entity;
using System.Diagnostics.Contracts;

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

            throw new FormatException("No data has been provided for the post title!"); //Also used in edit post

        }

        public Task<Post> TitleNeedsToHaveMoreThanThreeChars() //Also used in edit post
        {

            throw new FormatException("The provided title needs to have three or more characters");

        }

        public Task<Post> UnverifiedOwner() //Also used in edit post and delete post
        {

            throw new UnauthorizedAccessException("Post owner's email address is not verified!");

        }

        //Edit Post

        public Task<Post> PostDoesNotExist()
        {

            throw new ArgumentNullException("The post cannot be edited because it does not exist or was deleted!"); //Also used in delete post

        }

        public Task<Post> EditPostOwnerDoesNotExist()
        {

            throw new ArgumentNullException("The post cannot be edited because its owner does not exist or was deleted!");

        }



    }
}
