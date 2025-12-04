using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Services.PostsComments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsCommentsController(IPostsCommentsService postsCommentsService) : ControllerBase
    {

        [HttpPost("create-PostComment")]
        public async Task<ActionResult<PostComment>> CreatePostComment(Guid authorId,Guid postId, PostCommentsDTO request)
        {

            var result= await postsCommentsService.CreatePostCommentService(authorId, postId, request);
            return Ok(result);

        }

        [HttpGet("get-All-Post-Comments")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostComments()
        {

            var result=await postsCommentsService.GetAllPostCommentsService();

            return Ok(result);

        }

        [HttpGet("get-All-Post-Comments-For-Specific-Post")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostCommentsInPost(Guid postId)
        {

            var result= await postsCommentsService.GetAllPostCommentsInPostService(postId);

            return Ok(result);

        }

        [HttpGet("get-All-Post-Comments-For-Specific-Post-By-User")]
        public async Task<ActionResult<List<PostComment>>> GetAllPostCommentsInPostByUser(Guid postId, Guid authorId)
        {

            var result = await postsCommentsService.GetAllPostCommentsByUserInPostService(postId,authorId);

            return Ok(result);

        }

        [HttpPatch("edit-Post-Comment")]
        public async Task<ActionResult<PostComment>> EditPostComment(Guid authorId, Guid postId, Guid Id, PostCommentsDTO request)
        {

            var result = await postsCommentsService.EditPostCommentService(authorId, postId, Id, request);

            return Ok(result);

        }

        [HttpDelete("delete-Post-Comment")]
        public async Task<ActionResult<PostComment>> DeletePostComment(Guid authorId,Guid postId,Guid Id)
        {

            var result = await postsCommentsService.DeletePostCommentService(authorId, postId, Id);

            return Ok(result);

        }

    }
}
