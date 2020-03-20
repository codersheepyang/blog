using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.inputs;
using blog.Models.Article;
using blog.Models.Comment;
using blog.Services;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(ArticleController));


        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpPost("comment")]
        public ActionResult<string> AddComment(Comment comment)
        {
            if (comment.Content != null && comment.ArticleId != 0 && comment.FirstComment != false)
            {
                comment.CreateTime = DateTime.Now;
                string result = _articleService.AddComment(comment);
                return Ok(result);
            }
            return NotFound("评论内容、文章Id为空或不是第一评论者");
        }

        /*
        [HttpPost("commentReply")]
        public ActionResult<string> AddCommentReply(AddCommentReply addCommentReply)
        {
            if (addCommentReply.RepliedId != 0 && addCommentReply.ArticleId != 0 && addCommentReply.Content != null && addCommentReply.FirstComment == false)
            {
                if (addCommentReply.CommentName == null)
                {
                    addCommentReply.CommentName = "ReplyAnonymous";
                }
                addCommentReply.CreateTime = DateTime.Now;
                string result = _articleService.AddCommentReply(addCommentReply);
                return Ok(result);
            }
            return NotFound("传参存在空值");
        }
        */

        [HttpGet("article/{articleId}")]
        public ActionResult<string> GetArticle(int articleId)
        {
            if (articleId != 0)
            {
                string result =  _articleService.GetArticleById(articleId);
                return Ok(result);
            }
            return "获取文章Id为空";
        }

        [HttpGet("aboutMe")]
        public ActionResult<string> GetAboutMe()
        {
            AboutMe aboutMe =  _articleService.GetAboutMe();
            return Ok(aboutMe);
        }

        [HttpGet("articleByReadCounts/{userId}")]
        public ActionResult<string> GetArticleByReadCounts(int userId)
        {
            string result = _articleService.GetAllArticlesByReadCounts(userId);
            return Ok(result);
        }

        [HttpGet("articleByUpdateTime/{userId}")]
        public ActionResult<string> GetArticleByUpdateTime(int userId)
        {
            string result = _articleService.GetAllArticlesByUpdateTime(userId);
            return Ok(result);
        }

        [HttpGet("allClassification/{userId}")]
        public ActionResult<string> GetAllClassification(int userId)
        {
            string result = _articleService.GetAllClassification(userId);
            return Ok(result);
        }

        [HttpGet("latestArticle")]
        public ActionResult<string> GetLatestArticle()
        {
            string result = _articleService.GetLatestArticle();
            return Ok(result);
        }
        [HttpGet("allCommentsByArticleId/{articleId}")]
        public ActionResult<string> GetAllCommentsByArticleId(int articleId)
        {
            string result = _articleService.GetAllCommentsByArticleId(articleId);
            return Ok(result);
        }

        [HttpGet("classifications")]
        public ActionResult<string> GetAllClassfications()
        {
            string result = _articleService.GetAllClassfications();
            return Ok(result);
        }

        [HttpGet("tags")]
        public ActionResult<string> GetAllTags()
        {
            string result = _articleService.GetAllTags();
            return Ok(result);
        }

        [HttpGet("articlesByTagId/{tagId}")]
        public ActionResult<string> GetArticlesByTag(int tagId)
        {
            List<Dictionary<string, object>> result = _articleService.GetArticlesByTagId(tagId);
            return Ok(result);
        }
    }
}