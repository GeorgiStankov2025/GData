using GData.Data;
using GData.DTOs.PostsDTO;
using GData.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GData.Repositories.PostsComments
{
    public class PostsCommentsRepository(GDataDbContext dbContext) : IPostsCommentsRepository
    {
        public async Task<PostComment> CreatePostComment(PostComment postComment)
        {

            await dbContext.PostComments.AddAsync(postComment);
            await dbContext.SaveChangesAsync();

            return postComment;

        }

        public async Task<PostComment> DeletePostComment(PostComment postComment)
        {
            
           dbContext.PostComments.Remove(postComment);
           await dbContext.SaveChangesAsync();
           
           return postComment;
        
        }

        public async Task<PostComment> EditPostComment(PostCommentsDTO request,PostComment postComment)
        {

            postComment.Content = request.CommentContent;
            postComment.DateModified = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return postComment;

        }

        public async Task<List<PostComment>> GetAllPostComments()
        {
            
            return await dbContext.PostComments.Include<PostComment,User>(pc=>pc.Author).Include<PostComment,Post>(pc=>pc.Post).ToListAsync();

        }

        public async Task<PostComment> GetPostCommentById(Guid Id)
        {

            return await dbContext.PostComments.Include<PostComment,User>(pc=>pc.Author).Include<PostComment, Post>(pc=>pc.Post).FirstOrDefaultAsync(pc => pc.Id == Id);

        }
    }
}
