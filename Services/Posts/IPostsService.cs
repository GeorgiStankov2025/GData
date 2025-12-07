using GData.DTOs.PostDTO;
using GData.Entity;

namespace GData.Services.Posts
{
    public interface IPostsService
    {

        public Task<Post> CreatePostService(Guid OwnerId, PostDTO request);
        public Task<Post> GetPostById(Guid Id);
        public Task<List<Post>> GetAllPosts();
        public Task<List<Post>> GetAllPostsByUser(Guid ownerId);
        public Task<Post> UpdatePostService(Guid ownerId,PostDTO request, Guid Id);
        public Task<Post> DeletePostService(Guid ownerId,Guid Id);

    }
}
