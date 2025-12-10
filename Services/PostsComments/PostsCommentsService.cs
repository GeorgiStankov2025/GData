using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.PostsComments;
using GData.Services.Posts;
using GData.Services.Users;

namespace GData.Services.PostsComments
{
    public class PostsCommentsService(IPostsCommentsRepository postsCommentsRepository,IAuthServices authServices,IPostsService postsServices, PostCommentsExceptionList postCommentsExceptionList) : IPostsCommentsService
    {
        public async Task<PostComment> CreatePostCommentService(Guid authorId, Guid postId, PostCommentsDTO request)
        {

            var author = await authServices.GetUserByIdService(authorId);

            var post = await postsServices.GetPostById(postId);

            if (author is null)
            {

                return await postCommentsExceptionList.CreatePostCommentAuthorDoesNotExist();

            }

            if (post is null)
            {

                return await postCommentsExceptionList.CreatePostCommentPostDoesNotExist();

            }

            if (string.IsNullOrWhiteSpace(request.CommentContent))
            {

                return await postCommentsExceptionList.NoContentHasBeenProvidedForPostComment();

            }

            if(request.CommentContent.Length<3)
            {

                return await postCommentsExceptionList.ContentNeedsToHaveMoreThanThreeChars();

            }

            if (author.IsEmailConfirmed is false)
            {

                return await postCommentsExceptionList.UnverifiedAuthor();

            }

            var postComment = new PostComment()
            {

                AuthorId= authorId,
                PostId= postId,
                Content=request.CommentContent,
                DateCreated= DateTime.UtcNow,

            };

            await postsCommentsRepository.CreatePostComment(postComment);

            return postComment;

        }

        public async Task<PostComment> EditPostCommentService(Guid authorId, Guid postId, Guid Id, PostCommentsDTO request)
        {

            var postComment = await GetPostCommentById(Id);

            var author = await authServices.GetUserByIdService(authorId);

            var post = await postsServices.GetPostById(postId);

            if (postComment is null)
            {

                return await postCommentsExceptionList.PostCommentDoesNotExist();

            }

            if (postComment.PostId != postId)
            {

                return await postCommentsExceptionList.PostNotValid();

            }

            if (postComment.AuthorId != authorId)
            {

                return await postCommentsExceptionList.AuthorNotValid();

            }

            if (post is null)
            {

                return await postCommentsExceptionList.EditPostCommentPostDoesNotExist();

            }

            if (author is null)
            {

                return await postCommentsExceptionList.EditPostCommentAuthorDoesNotExist();

            }

            if (string.IsNullOrWhiteSpace(request.CommentContent))
            {

                return await postCommentsExceptionList.NoContentHasBeenProvidedForPostComment();

            }

            if (request.CommentContent.Length < 3)
            {

                return await postCommentsExceptionList.ContentNeedsToHaveMoreThanThreeChars();

            }

            if (author.IsEmailConfirmed is false)
            {

                return await postCommentsExceptionList.UnverifiedAuthor();

            }

            var result = await postsCommentsRepository.EditPostComment(request, postComment);

            return result;

        }

        public async Task<List<PostComment>> GetAllPostCommentsByUserInPostService(Guid postId, Guid authorId)
        {
            List<PostComment> selectedPostComments = new List<PostComment>();

            var postComments = await GetAllPostCommentsInPostService(postId);

            foreach (var postComment in postComments)
            {

                if (postComment.AuthorId == authorId&&postComment.PostId==postId)
                {

                    selectedPostComments.Add(postComment);

                }

            }

            return selectedPostComments;
        }

        public async Task<List<PostComment>> GetAllPostCommentsInPostService(Guid postId)
        {

            List<PostComment> selectedPostComments= new List<PostComment>();

            var postComments = await postsCommentsRepository.GetAllPostComments();

            foreach(var postComment in postComments)
            {

                if(postComment.PostId==postId)
                { 
                
                    selectedPostComments.Add(postComment);

                }

            }

            return selectedPostComments;

        }

        public async Task<List<PostComment>> GetAllPostCommentsService()
        {
            
            return await postsCommentsRepository.GetAllPostComments();

        }

        public async Task<PostComment> GetPostCommentById(Guid Id)
        {

            var postComment = await postsCommentsRepository.GetPostCommentById(Id);

            return postComment;

        }

        public async Task<PostComment> DeletePostCommentService(Guid authorId, Guid postId, Guid Id)
        {
            var author = await authServices.GetUserByIdService(authorId);

            var post = await postsServices.GetPostById(postId);

            var postComment = await GetPostCommentById(Id);

            if (postComment is null)
            {

                return await postCommentsExceptionList.PostCommentDoesNotExist();

            }

            if (postComment.PostId != postId)
            {

                return await postCommentsExceptionList.PostNotValid();

            }

            if (postComment.AuthorId != authorId)
            {

                return await postCommentsExceptionList.AuthorNotValid();

            }

            if (post is null)
            {

                return await postCommentsExceptionList.EditPostCommentPostDoesNotExist();

            }

            if (author is null)
            {

                return await postCommentsExceptionList.EditPostCommentAuthorDoesNotExist();

            }

            await postsCommentsRepository.DeletePostComment(postComment);

            return postComment;

        }

    }
}
