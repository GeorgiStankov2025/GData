using GData.DTOs.PostDTO;
using GData.DTOs.UserDTO;
using GData.Entity;
using GData.Exceptions;
using GData.Repositories.Posts;
using GData.Services.Users;

namespace GData.Services.Posts
{
    public class PostsService(IPostsRepository postsRepository,IAuthServices authServices,PostsExceptionList postsExceptionList) : IPostsService
    {
        public async Task<Post> CreatePostService(Guid OwnerId, PostDTO request)
        {

            User owner = await authServices.GetUserByIdService(OwnerId);

            if (owner == null)
            {

                return await postsExceptionList.CreatePostOwnerDoesNotExist();

            }

            if(string.IsNullOrWhiteSpace(request.Title))
            {

                return await postsExceptionList.NoTitleHasBeenProvidedForPost();

            }

            if(request.Title.Length<3)
            {

                return await postsExceptionList.TitleNeedsToHaveMoreThanThreeChars();

            }
            if(owner.IsEmailConfirmed==false)
            {

                return await postsExceptionList.UnverifiedOwner();

            }

            var post= new Post()
            {
                OwnerId = OwnerId,
                Title = request.Title,
                DateCreated = DateTime.UtcNow,

            };

            await postsRepository.CreatePost(post);
            return post;

        }

        public async Task<Post> DeletePostService(Guid ownerId, Guid Id)
        {

            var owner= await authServices.GetUserByIdService(ownerId);

            var post=await postsRepository.GetPostById(Id);

            if(owner is null)
            {

                return await postsExceptionList.EditPostOwnerDoesNotExist();

            }

            if(ownerId!=owner.Id)
            {

                return await postsExceptionList.OwnerNotValid();

            }

            if(post is null)
            {

                return await postsExceptionList.PostDoesNotExist();

            }

            if(owner.IsEmailConfirmed==false)
            {

                return await postsExceptionList.UnverifiedOwner();

            }

            await postsRepository.DeletePost(post);

            return post;

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

            if(post is null)
            {

                return await postsExceptionList.PostDoesNotExist();

            }

            return post;

        }

        public async Task<Post> UpdatePostService(Guid ownerId,PostDTO request,Guid Id)
        {

            if(string.IsNullOrWhiteSpace(request.Title))
            {

                return await postsExceptionList.NoTitleHasBeenProvidedForPost();

            }

            if(request.Title.Length<3)
            {

                return await postsExceptionList.TitleNeedsToHaveMoreThanThreeChars();

            }

            var post=await GetPostById(Id);

            if(post.Owner is null)
            {

                return await postsExceptionList.EditPostOwnerDoesNotExist();

            }

            if(ownerId!=post.OwnerId)
            {

                return await postsExceptionList.OwnerNotValid();

            }

            if(post is null)
            {

                return await postsExceptionList.PostDoesNotExist();

            }

            if(post.Owner.IsEmailConfirmed==false)
            {

                return await postsExceptionList.UnverifiedOwner();

            }

            await postsRepository.EditPost(request,post);

            return post;

        }
    }
}
