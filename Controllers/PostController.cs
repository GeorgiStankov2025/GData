// PostController.cs
using GData.DTOs.PostDTO;
using GData.Entity;
using GData.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostsService postsService) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var result = await postsService.GetPostById(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await postsService.GetAllPosts();
            return result.Count == 0 ? NotFound() : Ok(result);
        }

        [HttpGet("owner/{ownerId:guid}")]
        public async Task<IActionResult> GetAllPostsForUser(Guid ownerId)
        {
            var result = await postsService.GetAllPostsByUser(ownerId);
            return result.Count == 0 ? NoContent() : Ok(result);
        }

        [Authorize]
        [HttpPost("owner/{ownerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        public async Task<IActionResult> CreatePost(Guid ownerId, [FromBody] PostDTO request)
        {
            var result = await postsService.CreatePostService(ownerId, request);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{id:guid}/owner/{ownerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        public async Task<IActionResult> EditPost(Guid ownerId, Guid id, [FromBody] PostDTO request)
        {
            var result = await postsService.UpdatePostService(ownerId, request, id);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id:guid}/owner/{ownerId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Post))]
        public async Task<IActionResult> DeletePost(Guid ownerId, Guid id)
        {
            var result = await postsService.DeletePostService(ownerId, id);
            return Ok(result);
        }
    }
}