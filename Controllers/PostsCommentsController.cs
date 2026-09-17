// PostsCommentsController.cs
using GData.DTOs.PostsDTO;
using GData.Entity;
using GData.Services.PostsComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsCommentsController(IPostsCommentsService postsCommentsService) : ControllerBase
    {
        [Authorize]
        [HttpPost("posts/{postId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        public async Task<IActionResult> CreatePostComment(Guid authorId, Guid postId, [FromBody] PostCommentsDTO request)
        {
            var result = await postsCommentsService.CreatePostCommentService(authorId, postId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostComments()
        {
            var result = await postsCommentsService.GetAllPostCommentsService();
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("posts/{postId:guid}")]
        public async Task<IActionResult> GetAllPostCommentsInPost(Guid postId)
        {
            var result = await postsCommentsService.GetAllPostCommentsInPostService(postId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [HttpGet("posts/{postId:guid}/authors/{authorId:guid}")]
        public async Task<IActionResult> GetAllPostCommentsInPostByUser(Guid postId, Guid authorId)
        {
            var result = await postsCommentsService.GetAllPostCommentsByUserInPostService(postId, authorId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/posts/{postId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        public async Task<IActionResult> EditPostComment(Guid authorId, Guid postId, Guid id, [FromBody] PostCommentsDTO request)
        {
            var result = await postsCommentsService.EditPostCommentService(authorId, postId, id, request);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/posts/{postId:guid}/authors/{authorId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostComment))]
        public async Task<IActionResult> DeletePostComment(Guid authorId, Guid postId, Guid id)
        {
            var result = await postsCommentsService.DeletePostCommentService(authorId, postId, id);
            return Ok(result);
        }
    }
}