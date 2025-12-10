using GData.DTOs.ArticlesDTO;
using GData.Entity;
using GData.Services.ArticlesComments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesCommentsController(IArticlesCommentsServices articlesCommentsServices) : ControllerBase
    {

        [HttpPost("create-ArticleComment{authorId},{articleId}")]
        public async Task<ActionResult<ArticleComment>> CreateArticleComment(Guid authorId,Guid articleId,ArticleCommentDTO request)
        {

            var result=await articlesCommentsServices.CreateArticleCommentService(authorId, articleId, request);
            return Ok(result);

        }

        [HttpGet("get-All-ArticleComments")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleComments()
        {

            var result = await articlesCommentsServices.GetAllArticleCommentsService();

            return Ok(result);

        }

        [HttpGet("get-All-ArticleComments-InArticle{articleId}")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleCommentsInArticle(Guid articleId)
        {

            var result= await articlesCommentsServices.GetAllArticleCommentsInArticleService(articleId);
            return Ok(result);

        }

        [HttpGet("get-All-ArticleComments-ByUser-InArticle{authorId},{articleId}")]
        public async Task<ActionResult<List<ArticleComment>>> GetAllArticleCommentsInArticleByUser(Guid authorId,Guid articleId)
        {

            var result = await articlesCommentsServices.GetAllArticleCommentsInArticleByUserService(articleId,authorId);
            return Ok(result);

        }

        [HttpPatch("edit-ArticleComment{authorId},{articleId},{Id}")]
        public async Task<ActionResult<ArticleComment>> EditArticleComment(Guid authorId, Guid articleId, Guid Id, ArticleCommentDTO request)
        {

            var result=await articlesCommentsServices.EditArticleCommentService(authorId,articleId,Id,request);

            return Ok(result);

        }

        [HttpDelete("delete-Article-Comment{authorId},{articleId},{Id}")]
        public async Task<ActionResult<ArticleComment>> DeleteArticleComment(Guid authorId, Guid articleId, Guid Id)
        {

            var result= await articlesCommentsServices.DeleteArticleCommentService(authorId, articleId,Id);
            return Ok(result);

        }

    }
}
