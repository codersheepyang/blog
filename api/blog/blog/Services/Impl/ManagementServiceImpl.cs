using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.inputs;
using blog.Models;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using blog.Models.Tag;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace blog.Services.Impl
{
    public class ManagementServiceImpl : IManagementService
    {
        private readonly ArticleContext _articleContext;

        private readonly ClassificationContext _classificationContext;

        private readonly CommentContext _commentContext;

        private readonly UserContext _userContext;

        private readonly TagContext _tagContext;
        private const string SHEMA = "bank_cookie";


        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(ManagementServiceImpl));

        public ManagementServiceImpl(ArticleContext articleContext, ClassificationContext classificationContext
                                   , CommentContext commentContext,UserContext userContext,TagContext tagContext)
        {
            _articleContext = articleContext;
            _classificationContext = classificationContext;
            _commentContext = commentContext;
            _userContext = userContext;
            _tagContext = tagContext;
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="article"></param>
        public string AddArticle(Article article)
        {
            var check = _classificationContext.Classification.Where(c => c.Id == article.ClassificationId && article.UserId == c.UserId).FirstOrDefault();
            if (check == null)
            {
                return "添加文章的分类不存在";
            }
            _articleContext.Add(article);
            if (_articleContext.SaveChanges() == 1)
            {
                return "文章添加成功";
            }
            return "文章添加失败";
        }


        /// <summary>
        /// 添加分类
        /// 在传入相同分类名是会抛运行时异常
        /// return 0 添加文章失败 1 添加文章成功
        /// </summary>
        /// <param name="classification"></param>
        public string AddClassification(Classification classification)
        {
            //判断添加的分类是否存在
            var check = _classificationContext.Classification.Where(c => c.ClassificationName == classification.ClassificationName && c.UserId == classification.UserId).FirstOrDefault();
            if (check != null)
            {
                return "添加的分类已经存在";
            }
            _classificationContext.Add(classification);
            if (_classificationContext.SaveChanges() == 1)
            {
                return "添加分类成功";
            }
            return "添加分类失败";
        }

        /// <summary>
        /// 通过文章Id删除文章
        /// </summary>
        /// <param name="articleId"></param>
        public string DeleteArticleById(int articleId)
        {
            return DeleteArticle(articleId);
        }

        /// <summary>
        /// 通过分类Id删除分类
        /// </summary>
        /// <param name="classificationId"></param>
        /// <returns></returns>
        public string DeleteClassificationById(int classificationId)
        {
            //1.判断是否存在分类
            var deleteClassification = _classificationContext.Classification.Where(c => c.Id == classificationId).FirstOrDefault();
            if (deleteClassification != null)
            {
                //2.判断删除的分类是否包含文章
                List<Article> articles = _articleContext.Article.Where(a => a.ClassificationId == classificationId).ToList();
                if (articles.Count != 0)
                {
                    //存在文章，需要先删除
                    foreach (Article article in articles)
                    {
                        DeleteArticle(article.Id);
                    }
                }
                //4.删除分类
                _classificationContext.Classification.Remove(deleteClassification);
                if (_classificationContext.SaveChanges() == 1)
                {
                    return "分类删除成功";
                }
                return "分类删除失败";
            }

            return "删除的分类不存在";
        }

        public string DeleteArticle(int articleId)
        {
            var deleteArticle = _articleContext.Article.Where(a => a.Id == articleId).FirstOrDefault();
            //判断该文章是否存在
            if (deleteArticle != null)
            {
                //3.判断该文章是否存在评论
                List<Comment> comments = _commentContext.Comment.Where(c => c.ArticleId == deleteArticle.Id).ToList();
                if (comments != null)
                {
                    //删除评论
                    foreach (Comment comment in comments)
                    {
                        _commentContext.Comment.Remove(comment);
                    }
                    _commentContext.SaveChanges();
                }
                //删除文章 
                _articleContext.Article.Remove(deleteArticle);
                if (_articleContext.SaveChanges() == 1)
                {
                    return "文章删除成功";
                }
                return "文章删除失败";
            }
            return "删除的文章Id不存在";
        }

        //获取所有文章的基本信息
        public List<Dictionary<string, object>> GetAllArticlesMessage(int userId)
        {
            List<Dictionary<string, object>> allArticlesMessage = new List<Dictionary<string, object>>();
            List<Article> articles = _articleContext.Article.Where(art => art.UserId == userId).ToList();
            foreach (Article article in articles)
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                keyValuePairs.Add("articleId", article.Id);
                string content;
                if (article.Content.Length < 100)
                {
                    content = article.Content;
                }
                else
                {
                    content = article.Content.Substring(0, 99);
                }
                keyValuePairs.Add("content", content);
                keyValuePairs.Add("articleName", article.ArticleName);
                string createTime = article.InDate.Date.ToString().Substring(0,9);
                keyValuePairs.Add("inDate", createTime);
                keyValuePairs.Add("postStatus", article.Status);
                keyValuePairs.Add("browseNumber", article.BrowseNumber);
                int counts = _commentContext.Comment.Where(c => c.ArticleId == article.Id).Count();
                keyValuePairs.Add("commentCounts", counts);
                allArticlesMessage.Add(keyValuePairs);
            }
            return allArticlesMessage;
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        public string GetAllClassifications(int userId)
        {
            List<Classification> classifications = _classificationContext.Classification.Where(c => c.UserId == userId).ToList();
            string json;
            if (classifications != null)
            {
                json = JsonConvert.SerializeObject(classifications);
                return json;
            }
            json = "不存在任何分类";
            return json;
           
        }

        /// <summary>
        /// 获取所有文章的所有评论
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string,object>> GetAllComments(int userId)
        {

            List<Dictionary<string, object>> allComments = null;
            //获取所有文章
            List<Article> articles = _articleContext.Article.Where(art => art.UserId == userId).ToList();
            foreach (Article article in articles)
            {
                //根据文章获取第一评论
                List<Comment> comments = _commentContext.Comment.Where(c => c.ArticleId == article.Id && c.FirstComment == true).ToList();
                if (comments != null && comments.Count != 0)
                {
                    if (allComments == null)
                    {
                        allComments = new List<Dictionary<string, object>>();
                    }
                    foreach (Comment comment in comments)
                    {
                        Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                        Article current = _articleContext.Article.Where(a => a.Id == comment.ArticleId).FirstOrDefault();
                        keyValuePairs.Add("id", comment.Id);
                        keyValuePairs.Add("articleName", current.ArticleName);
                        keyValuePairs.Add("commentName", comment.CommentName);
                        keyValuePairs.Add("content", comment.Content);
                        keyValuePairs.Add("status", comment.Status);
                        allComments.Add(keyValuePairs);
                    }
                }
            }
            return allComments;
        }

        /// <summary>
        /// 通过文章Id获取文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public string GetArticleById(int articleId)
        {
            Article result = _articleContext.Article.Find(articleId);
            string json;
            if (result != null)
            {
                json = JsonConvert.SerializeObject(result);
                return json;
            }
            json = "文章不存在";
            return json;
        }

       /// <summary>
       /// 通过分类Id获取分类
       /// </summary>
       /// <param name="classificationId"></param>
       /// <returns></returns>
        public string GetClassificationById(int classificationId)
        {
            Classification result = _classificationContext.Classification.Find(classificationId);
            string json;
            if (result != null)
            {
                Object classification = new { Id = result.Id, ClassificationName = result.ClassificationName };
                json = JsonConvert.SerializeObject(classification);
                return json;
            }
            json = "分类不存在";
            return json;
        }

        /// <summary>
        /// 更新文章
        /// 必须提供的数据：文章名字、文章内容(文章内容可空)，分类Id
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public string UpdateArticle(Article article)
        {
            string articleName = article.ArticleName;
            string content = article.Content;
            int classificationId = article.ClassificationId;
            int articleId = article.Id;

            var updateArticle = _articleContext.Article.Where(a => a.Id == articleId).FirstOrDefault();
            if (updateArticle != null)
            {
                updateArticle.ArticleName = articleName;
                updateArticle.Content = content;
                updateArticle.ClassificationId = classificationId;

                if (_articleContext.SaveChanges() == 1)
                {
                    return "文修改成功";
                }
                return "文章修改失败,可能是文章未做任何修改";
            }
            return "修改的文章Id不存在";

        }


        /// <summary>
        /// 通过分类Id更新分类名字
        /// </summary>
        /// <param name="classification"></param>
        /// <returns></returns>
        public string UpdateClassification(Classification classification)
        {
            string classificationName = classification.ClassificationName;
            int userId = classification.UserId;
            int classificationId = classification.Id;
            int check = _classificationContext.Classification.Where(c => c.ClassificationName == classificationName && c.UserId == userId).Count();
            if (check > 0) {
                return "修改的分类名称已存在";
            }
            var updateClassification = _classificationContext.Classification.Where(c => c.Id == classificationId && c.UserId == userId).FirstOrDefault();
            if(updateClassification != null)
            {
                updateClassification.ClassificationName = classificationName;
                if (_classificationContext.SaveChanges() == 1)
                {
                    return "分类修改成功";
                }
                return "分类修改失败，可能是分类未做任何修改";
            }
            return "修改的分类不存在";
            
        }

        public void UpdateCommentStatus(Comment comment)
        {
           Comment com = _commentContext.Comment.Where(c => c.Id == comment.Id).FirstOrDefault();
            if (com != null)
            {
                com.Status = "已读";
            }
            _commentContext.SaveChanges();
        }

        /// <summary>
        /// 通过评论Id删除评论
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public string DeleteCommentByCommentId(int commentId)
        {
            Comment comment = _commentContext.Comment.Where(c => c.Id == commentId).FirstOrDefault();
            if (comment != null)
            {
                _commentContext.Comment.Remove(comment);
                if (_commentContext.SaveChanges() == 1)
                {
                    return "评论删除成功";
                }
                return "评论删除失败";
            }
            return "删除的评论不存在";
        }


        /// <summary>
        /// 获得所有浏览量
        /// </summary>
        /// <returns></returns>
        public string GetBrowseNumbers(int userId)
        {
            int browseNumbers = 0;
            List<Article> articles = _articleContext.Article.Where(a => a.UserId == userId).ToList();
            if (articles != null && articles.Count != 0)
            {
                foreach (Article article in articles)
                {
                    browseNumbers += article.BrowseNumber;
                }
                string result = JsonConvert.SerializeObject(browseNumbers);
                return result;
            }
            return "不存在文章";
        }

        public int AddPersonalMessage(User user)
        {
            User originalUser = _userContext.User.Where(u => u.ID == user.ID).FirstOrDefault();
            if (originalUser != null)
            {
                originalUser.SelfIntroduction = user.SelfIntroduction ?? originalUser.SelfIntroduction;
                originalUser.Email = user.Email ?? originalUser.Email;
                originalUser.Name = user.Name ?? originalUser.Name;
                originalUser.Location = user.Location ?? originalUser.Location;
                originalUser.Company = user.Company ?? originalUser.Company;
                if (_userContext.SaveChanges() == 1)
                {
                    return 1;
                }
                return -1;
            }
            return -1;
        }

        public string GetPersonalMessage(int userId)
        {
            User user = _userContext.User.Where(u => u.ID == userId).FirstOrDefault();
            if (user != null && (user.Email != null || user.SelfIntroduction != null || user.Location != null
                    || user.Name != null || user.Company != null))
            {
                string json = JsonConvert.SerializeObject(user);
                return json;
            }
            return null;
        }

        public string GetTagByTagId(int tagId)
        {
            Tag tag = _tagContext.Tag.Where(t => t.ID == tagId).FirstOrDefault();
            string json = JsonConvert.SerializeObject(tag);
            return json;
        }
    }
}
