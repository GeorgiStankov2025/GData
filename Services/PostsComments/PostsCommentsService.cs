using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Repositories.PostsComments;

namespace GData.Services.PostsComments
{
    public class PostsCommentsService(IPostsCommentsRepository postsCommentsRepository) : IPostsCommentsService
    {
        public async Task<PostComment> CreatePostCommentService(Guid authorId, Guid postId, PostCommentsDTO request)
        {

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

        public Task<PostComment> DeletePostComment(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PostComment> EditPostCommentService(Guid authorId, Guid postId, Guid Id, PostCommentsDTO request)
        {

            var postComment = await GetPostCommentById(Id);

            var result = await postsCommentsRepository.EditPostComment(request, postComment);

            return result;

        }

        public async Task<List<PostComment>> GetAllPostCommentsByUserInPostService(Guid postId, Guid authorId)
        {
            List<PostComment> selectedPostComments = new List<PostComment>();

            var postComments = await postsCommentsRepository.GetAllPostComments();

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

            var postComment = await GetPostCommentById(Id);

            await postsCommentsRepository.DeletePostComment(postComment);

            return postComment;

        }

    }
}
