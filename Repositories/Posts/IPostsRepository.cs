using GData.DTOs.PostDTO;
using GData.Entity;

namespace GData.Repositories.Posts
{
    public interface IPostsRepository
    {
        public Task<Post> CreatePost(Post post);
        public Task<List<Post>> GetAllPosts();
        public Task<Post> GetPostById(Guid Id);
        public Task<Post> GetPostByOwnerId(Guid ownerId);
        public Task<Post> GetPostByTitle(string title);
        public Task<Post> EditPost(PostDTO request,Post post);
        public Task<Post> DeletePost(Post post);

    }
}
