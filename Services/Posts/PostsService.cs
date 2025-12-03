using GData.DTOs.PostDTO;
using GData.Entity;
using GData.Repositories.Posts;
using GData.Services.Users;

namespace GData.Services.Posts
{
    public class PostsService(IPostsRepository postsRepository,IAuthServices authServices) : IPostsService
    {
        public async Task<Post> CreatePostService(Guid OwnerId, PostDTO request)
        {

            User owner = await authServices.GetUserByIdService(OwnerId);

            var post= new Post()
            {
                OwnerId = OwnerId,
                Title = request.Title,
                DateCreated = DateTime.UtcNow,

            };

            await postsRepository.CreatePost(post);
            return post;

        }

        public Task<Post> DeletePostService(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            
            return await postsRepository.GetAllPosts();

        }

        public async Task<List<Post>> GetAllPostsByUser(Guid ownerId)
        {

            List<Post> selectedPosts = new List<Post>();

            var posts=await GetAllPosts();

            foreach (var post in posts)
            {

                if(post.OwnerId == ownerId)
                {

                    selectedPosts.Add(post);

                }

            }

            return selectedPosts;

        }

        public async Task<Post> GetPostById(Guid Id)
        {
           
            var post= await postsRepository.GetPostById(Id);

            return post;

        }

        public async Task<Post> UpdatePostService( PostDTO request)
        {

            throw new Exception();

        }
    }
}
