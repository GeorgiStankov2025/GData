using GData.DTOs.PostsDTO;
using GData.Entity;

namespace GData.Services.PostsComments
{
    public interface IPostsCommentsService
    {
        public Task<PostComment> CreatePostCommentService(Guid ownerId,Guid postId,PostCommentsDTO request);
        public Task<PostComment> EditPostCommentService(Guid ownerId, Guid postId, Guid Id, PostCommentsDTO request);
        public Task<PostComment> DeletePostComment(Guid Id);
        public Task<List<PostComment>> GetAllPostCommentsService();
        public Task<List<PostComment>> GetAllPostCommentsInPostService(Guid postId);
        public Task<List<PostComment>> GetAllPostCommentsByUserInPostService(Guid postId, Guid authorId);
        public Task<PostComment> GetPostCommentById(Guid Id);
        public Task<PostComment> DeletePostCommentService(Guid authorId, Guid postId, Guid Id);

    }
}
