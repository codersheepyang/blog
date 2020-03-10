using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using blog.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementService _managementService; 

        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(ManagementController));


        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }


        [HttpPost("classification")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<string> AddClassification(Classification classification)
        {
            log.Info("添加的分类为" + classification.ClassificationName);
            string classificationName = classification.ClassificationName;
            if (classificationName != null)
            {
                string result = _managementService.AddClassification(classification);
                return Ok(result);
            }
            return NotFound("添加的分类名为空");
        }


        [HttpPost("article")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<string> AddArticle(Article article)
        {
            //string articleName, string content, string createUser, int classificationId,status
            string articleName = article.ArticleName;
            string CreateUser = article.InUser;
            int classificationId = article.ClassificationId;
            int userId = article.UserId;
            article.InDate = DateTime.Now;

            log.Info("添加的文章名为:" + articleName + ",添加的用户是:" + CreateUser + ",添加的分类Id为:" + classificationId);
            if (userId != 0 && articleName != null && CreateUser != null && classificationId != 0)
            {
                string result = _managementService.AddArticle(article);
                return Ok(result);
            }
            return NotFound("添加的文章名、用户名或分类Id为空");
        }

        [HttpDelete("article/{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteArticle(int id) 
        {
            log.Info("删除的文章Id是：" + id);
            if (id != 0)
            {
              string result = _managementService.DeleteArticleById(id);
                return Ok(result);
            }
            return NotFound("删除的文章Id为空");
        }

        [HttpDelete("classification/{id}")]
        public ActionResult<string> DeleteClassification(int id)
        {
            log.Info("删除的分类Id是:" + id);
            string result = _managementService.DeleteClassificationById(id);
            return Ok(result);
        }

        [HttpGet("classifications/{userId}")]
        public ActionResult<string> GetAllClassifications(int userId) 
        {
            string json = _managementService.GetAllClassifications(userId);
            return Ok(json);
        }

        [HttpGet("article/{articleId}")]
        public ActionResult<string> Article(int articleId)
        { 
            log.Info("获取的文章Id是:" + articleId);
            string json;
            if (articleId != 0)
            {
                json = _managementService.GetArticleById(articleId);
                return Ok(json);
            }
            json = "文章Id为空";
            return NotFound(json);
        }

        [HttpGet("classification/{classificationId}")]
        public ActionResult<string> Classification(int classificationId)
        {
            log.Info("获取分类的Id是:" + classificationId);
            string json;
            if(classificationId != 0)
            {
                json = _managementService.GetClassificationById(classificationId);
                return Ok(json);
            }
            json = "分类Id为空";
            return NotFound(json);
        }

        [HttpPut("article")]
        public ActionResult<string> UpdateArticle(Article article)
        {
            log.Info(article.ArticleName + article.Content + article.ClassificationId + article.Id);
            string json;
            if (article.ArticleName != null && article.ClassificationId != 0 && article.Id != 0 && article.UserId != 0)
            {
                string result = _managementService.UpdateArticle(article);
                return Ok(result);
            }
            json = "文章名、分类ID或者文章ID为空";
            return NotFound(json);
        }

        [HttpPut("classification")]
        public ActionResult<string> UpdateClassification(Classification classification)
        {
            string json;
            if (classification.Id != 0 && classification.ClassificationName != null)
            {
                string result = _managementService.UpdateClassification(classification);
                return Ok(result);
            }
            json = "分类ID或者分类名为空";
            return NotFound(json);
        }

        [HttpGet("comments/{userId}")]
        public ActionResult<string> GetAllComments(int userId)
        {
            List<Dictionary<string, object>> comments =  _managementService.GetAllComments(userId);
            if (comments != null)
            {
                return Ok(comments);
            }
            return NotFound("所有文章不存在评论");
        }

        [HttpGet("allArticleMessage/{userId}")]
        public ActionResult<string> GetAllArticlesMessage(int userId)
        {
            List<Dictionary<string, object>> allArticlesMessage = _managementService.GetAllArticlesMessage(userId);
            return Ok(allArticlesMessage);
        }

        [HttpDelete("comment/{id}")]
        public ActionResult<string> DeleteComment(int id)
        {

                string json = _managementService.DeleteCommentByCommentId(id);
                return Ok(json);
            /*if (comment.Id != 0)
            {
            }
            */

        }

        [HttpGet("browseNumbers")]
        public ActionResult<string> GetBrowseNumbers()
        {
            string json = _managementService.GetBrowseNumbers();
            return Ok(json);
        }

        [HttpPost("personalMessage")]
        public ActionResult<int> AddPersonalMessage(User user)
        {
            return Ok(_managementService.AddPersonalMessage(user));
        }

        [HttpGet("personalMessage/{userId}")]
        public ActionResult<string> GetPersonalMessage(int userId)
        {

        }
    }
}