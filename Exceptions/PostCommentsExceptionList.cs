using GData.Entity;

namespace GData.Exceptions
{
    public class PostCommentsExceptionList
    {

        //CreatePostComment

        public Task<PostComment> CreatePostCommentAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The post comment cannot be created because its author does not exist or was deleted!"); 
        }

        public Task<PostComment> CreatePostCommentPostDoesNotExist()
        {

            throw new ArgumentNullException("The post comment cannot be created because its post does not exist or was deleted!"); 

        }

        public Task<PostComment> NoContentHasBeenProvidedForPostComment()
        {

            throw new FormatException("No data has been provided for the post comment!"); //Also used in edit postcomment and delete postcomment

        }

        public Task<PostComment> ContentNeedsToHaveMoreThanThreeChars() 
        {

            throw new FormatException("The provided content needs to have three or more characters"); //Also used in edit postcomment and delete postcomment

        }

        public Task<PostComment> UnverifiedAuthor() 
        {

            throw new UnauthorizedAccessException("Post comment author's email address is not verified!"); //Also used in edit postcomment and delete postcomment

        }

        //Edit PostComment

        public Task<PostComment> PostCommentDoesNotExist()
        {

            throw new ArgumentNullException("The post comment cannot be edited because it does not exist or was deleted!"); //Also used in delete post comment

        }

        public Task<PostComment> EditPostCommentAuthorDoesNotExist()
        {

            throw new ArgumentNullException("The post comment cannot be edited because its author does not exist or was deleted!"); //Also used in delete post comment

        }

        public Task<PostComment> EditPostCommentPostDoesNotExist()
        {

            throw new ArgumentNullException("The post comment cannot be edited because its post does not exist or was deleted!"); //Also used in delete post comment

        }

        public Task<PostComment> AuthorNotValid()
        {

            throw new UnauthorizedAccessException("The user who is trying to edit the post comment is not it's author!"); //Also used in delete post comment

        }

        public Task<PostComment> PostNotValid()
        {

            throw new UnauthorizedAccessException("The edited comment does not belong to this post!"); //Also used in delete post comment

        }

    }
}
