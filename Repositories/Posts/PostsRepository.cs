using GData.Data;
using GData.DTOs.PostDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;

namespace GData.Repositories.Posts
{
    public class PostsRepository(GDataDbContext dbContext) : IPostsRepository
    {
        public async Task<Post> CreatePost(Post post)
        {

            await dbContext.Posts.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return post;

        }

        public async Task<Post> DeletePost(Post post)
        {

            dbContext.Posts.Remove(post);
            await dbContext.SaveChangesAsync();

            return null;

        }

        public async Task<Post> EditPost(PostDTO request, Post post)
        {
            
            post.Title = request.Title;
            await dbContext.SaveChangesAsync();
            return post;

        }

        public async Task<List<Post>> GetAllPosts()
        {

            return await dbContext.Posts.ToListAsync();

        }

        public Task<Post> GetPostById(Guid Id)
        {

            var post = dbContext.Posts.Include(p => p.Owner).FirstOrDefaultAsync(p=>p.Id==Id);

            return post;

        }

        public async Task<Post> GetPostByOwnerId(Guid ownerId)
        {
            
            var post = await dbContext.Posts.Include(p => p.Owner).FirstOrDefaultAsync(p=>p.OwnerId==ownerId);

            return post;

        }

        public async Task<Post> GetPostByTitle(string title)
        {

            var post = await dbContext.Posts.Include(p => p.Owner).FirstOrDefaultAsync(p => p.Title == title);

            return post;

        }
    }
}
