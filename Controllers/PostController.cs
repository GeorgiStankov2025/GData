using GData.DTOs.PostDTO;
using GData.Entity;
using GData.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController(IPostsService postsService) : ControllerBase
    {

        [HttpGet("get-Post-By-Id{Id}")]
        public async Task<ActionResult<Post>> GetPostById(Guid Id)
        {

            var result= await postsService.GetPostById(Id);
            return Ok(result);

        }

        [HttpGet("get-All-Posts")]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {

            var result = await postsService.GetAllPosts();
            return Ok(result);

        }

        [HttpGet("get-All-Posts-For-User{ownerId}")]
        public async Task<ActionResult<List<Post>>> GetAllPostsForUser(Guid ownerId)
        {

            var result=await postsService.GetAllPostsByUser(ownerId);
            return Ok(result);  

        }

        [HttpPost("create-Post{ownerId}")]
        public async Task<ActionResult<Post>> CreatePost(Guid ownerId, PostDTO request)
        {

            var result= await postsService.CreatePostService(ownerId, request);
            return Ok(result);

        }

    }
}
