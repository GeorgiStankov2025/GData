using GData.DTOs.PostsDTO;
using GData.Entity;

namespace GData.Repositories.PostsComments
{
    public interface IPostsCommentsRepository
    {
        public Task<PostComment> CreatePostComment(PostComment postComment);
        public Task<PostComment> EditPostComment(PostCommentsDTO request, PostComment postComment);
        public Task<PostComment> GetPostCommentById(Guid Id);
        public Task<List<PostComment>> GetAllPostComments();
        public Task<List<PostComment>> GetAllPostCommentsWithPostId(Guid Id);
        public Task<List<PostComment>> GetAllPostCommentsWithAuthorId(Guid Id);
        public Task<PostComment> DeletePostComment(PostComment postComment);

    }
}
