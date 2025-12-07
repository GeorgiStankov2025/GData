using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Services.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(IArticleServices articleServices) : ControllerBase
    {

        [HttpPost("create-Article{creatorId}")]
        public async Task<ActionResult<Article>> CreateArticle(Guid creatorId, ArticleDTO request)
        {

            var result = await articleServices.CreateArticleService(creatorId, request);
            return Ok(result);

        }

        [HttpGet("get-All-Articles")]
        public async Task<ActionResult<List<Article>>> GetAllArticles()
        {

            var result = await articleServices.GetAllArticlesService();
            return Ok(result);

        }
        [HttpGet("get-Article-By-Id{Id}")]
        public async Task<ActionResult<Article>> GetArticleById(Guid Id)
        {

            var result = await articleServices.GetArticleByIdService(Id);
            return Ok(result);

        }
        [HttpGet("get-Article-By-Title")]
        public async Task<ActionResult<Article>> GetArticleByTitle(string title)
        {

            var result = await articleServices.GetArticleByTitleService(title);
            return Ok(result);

        }

        [HttpPatch("edit-Article{creatorId},{Id}")]
        public async Task<ActionResult<Article>> EditArticle(Guid creatorId, Guid Id, ArticleDTO request)
        {

            var result = await articleServices.EditArticleService(creatorId, Id, request);
            return Ok(result);

        }

        [HttpDelete("delete-Article{creatorId},{Id}")]
        public async Task<ActionResult<Article>> DeleteArticle(Guid creatorId, Guid Id)
        {

            var result = await articleServices.DeleteArticleService(creatorId, Id);
            return Ok(result);

        }
    }
}
