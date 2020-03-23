using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using blog.inputs;
using blog.Models;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using blog.Models.Tag;
using log4net;
using Newtonsoft.Json;

namespace blog.Services.Impl
{
    public class ArticleServiceImpl : IArticleService
    {
        private readonly ArticleContext _articleContext;

        private readonly UserContext _userContext;

        private readonly ClassificationContext _classificationContext;

        private readonly CommentContext _commentContext;

        private readonly TagContext _tagContext;

        private readonly ReplyContext _replyContext;


        private const string SHEMA = "bank_cookie";

        private readonly ILog log = LogManager.GetLogger(Startup.Repository.Name, typeof(ArticleServiceImpl));

        public ArticleServiceImpl(ArticleContext articleContext, ClassificationContext classificationContext
                                   ,CommentContext commentContext,TagContext tagContext
                                   ,UserContext userContext
                                   ,ReplyContext replyContext)
        {
            _articleContext = articleContext;
            _classificationContext = classificationContext;
            _commentContext = commentContext;
            _tagContext = tagContext;
            _userContext = userContext;
            _replyContext = replyContext;
        }

        /// <summary>
        /// 添加一个新评论
        /// </summary>
        /// <param name="comment"></param>
        public string AddComment(Comment comment)
        {
            //判断添加评论的文章是否存在
            var check = _articleContext.Article.Where(a => a.Id == comment.ArticleId).FirstOrDefault();
            if (check == null)
            {
                return "添加评论的文章不存在";
            }
            string name = _userContext.User.Find(comment.UserId).Name;
            if (name == null)
            {
                comment.CommentName = "Anonymous";
            }
            comment.CommentName = name;
            _commentContext.Comment.Add(comment);
            if (_commentContext.SaveChanges() == 1)
            {
                return "评论添加成功";
            }
            return "评论添加失败";
        }

        /*
        public string AddCommentReply(AddCommentReply addCommentReply)
        {
            int repliedId = addCommentReply.RepliedId;
            int articleId = addCommentReply.ArticleId;
            string commentName = addCommentReply.CommentName;
            string mailBox = addCommentReply.MailBox;
            string content = addCommentReply.Content;
            bool firstComment = addCommentReply.FirstComment;
            DateTime createTime = addCommentReply.CreateTime;

            //判断回复的评论是否存在
            var check = _commentContext.Comment.Where(c => c.Id == repliedId).FirstOrDefault();
            if (check == null)
            {
                return "回复的评论不存在";
            }
                //1.添加回复评论
                Comment comment = new Comment
                {
                    ArticleId = articleId,
                    CommentName = commentName,
                    MailBox = mailBox,
                    Content = content,
                    FirstComment = firstComment,
                    CreateTime = createTime
                };
                //暂时未做事务处理
                _commentContext.Comment.Add(comment);
                if (_commentContext.SaveChanges() == 1)
                {
                    //获得添加的主键 
                    int replyId = comment.Id;
                    Reply reply = new Reply
                    {
                        ReplyId = replyId,
                        RepliedId = repliedId
                    };
                    _replyContext.Reply.Add(reply);
                    if (_replyContext.SaveChanges() == 1)
                    {
                        return "回复的评论添加成功";
                    }
                }
            return "回复的评论添加失败";
        }
        */

        public AboutMe GetAboutMe()
        {
            AboutMe aboutMe = new AboutMe
            {
                BlogerName = "Cookie",
                Address = "四川省成都市",
                IndividualResume = "每天都要加油鸭！",
                Email = "935046315@qq.com"
            };
            return aboutMe;
        }

        /// <summary>
        /// 通过阅读量显示文章列表 
        /// </summary>
        /// <returns></returns>
        public string GetAllArticlesByReadCounts(int userId)
        {
            List<Article> articles = _articleContext.Article.Where(art => art.UserId == userId).OrderByDescending(a => a.BrowseNumber).ToList();
            if (articles != null)
            {
                string json = JsonConvert.SerializeObject(articles);
                return json;
            }
            return "文章列表为空";
        }

        /// <summary>
        /// 通过更新时间显示文章列表 
        /// </summary>
        /// <returns></returns>
        public string GetAllArticlesByUpdateTime(int userId)
        {
            List<Article> articles = _articleContext.Article.Where(art => art.UserId == userId).OrderByDescending(a => a.InDate).ToList();
            if (articles != null)
            {
                string json = JsonConvert.SerializeObject(articles);
                return json;
            }
            return "文章列表为空";
        }

        /// <summary>
        /// 获得所有分类与分类包含的文章数量
        /// </summary>
        /// <returns></returns>
        public string GetAllClassification(int userId)
        {
            List<Dictionary<string, object>> keyValuePairs = new List<Dictionary<string, object>>();
            List<Classification> classifications = _classificationContext.Classification.Where(c => c.UserId == userId).ToList();
            if (classifications != null && classifications.Count() != 0)
            {
                foreach (Classification classification in classifications)
                {
                    int articleCounts = _articleContext.Article.Where(a => a.ClassificationId == classification.Id).Count();
                    Dictionary<string, object> record = new Dictionary<string, object>();
                    record.Add("classificationName", classification.ClassificationName);
                    record.Add("articleCounts", articleCounts);
                    record.Add("classificationId", classification.Id);
                    keyValuePairs.Add(record);
                }
                string json = JsonConvert.SerializeObject(keyValuePairs);
                return json;
            }
            return "不存在任何分类";
        }

        public string GetAllCommentsByArticleId(int articleId)
        {
            List<Dictionary<string, object>> record = new List<Dictionary<string, object>>();
            List<Comment> comments = _commentContext.Comment.Where(c => c.ArticleId == articleId).ToList();
            if (comments != null && comments.Count != 0) 
            {
                foreach (Comment comment in comments)
                {
                    Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();
                    keyValuePairs.Add("commentName", comment.CommentName);
                    keyValuePairs.Add("content", comment.Content);
                    record.Add(keyValuePairs);
                }
                string json = JsonConvert.SerializeObject(record);
                return json;
            }
            return "该文章不存在评论";
            
        }

        public string GetArticleById(int articleId)
        {
            //添加浏览量
            var article = _articleContext.Article.Where(a => a.Id == articleId).FirstOrDefault();
            if (article != null)
            {
                article.BrowseNumber += 1;
                if (_articleContext.SaveChanges() == 1)
                {
                    article.InDate = article.InDate.Date;
                    string json = JsonConvert.SerializeObject(article);
                    return json;
                }
                return "获取文章失败";
            }
            return "获取文章不存在";
        }

        public string GetLatestArticle()
        {
            Article article = _articleContext.Article.OrderByDescending(a => a.InDate).FirstOrDefault();
            if (article != null)
            {
                string json = JsonConvert.SerializeObject(article);
                return json;
            }
            return "不存在最新文章";
        }

        public Dictionary<string, object> GetPersonalData()
        {
            throw new NotImplementedException();
        }

        public List<Dictionary<string, object>> GetPigeonhole()
        {
            throw new NotImplementedException();
        }

        public string GetAllClassfications()
        {
            List<Classification> lists =  _classificationContext.Classification.Take(20).ToList();
            if (lists != null)
            {
                string json = JsonConvert.SerializeObject(lists);
                return json;
            }
            return null;
        }

        public string GetAllTags()
        {
            List<Tag> lists = _tagContext.Tag.OrderBy(t => t.ID).ToList();
            if (lists != null)
            {
                string json = JsonConvert.SerializeObject(lists);
                return json;
            }
            return null;
        }

        public List<Dictionary<string, object>> GetArticlesByTagId(int tagId)
        {
            List<Dictionary<string, object>> allArticlesMessage = new List<Dictionary<string, object>>();
            List<Article> articles = _articleContext.Article.Where(a => a.TagId == tagId).ToList();
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
                string createTime = article.InDate.Date.ToString().Substring(0, 9);
                keyValuePairs.Add("inDate", createTime);
                keyValuePairs.Add("postStatus", article.Status);
                keyValuePairs.Add("browseNumber", article.BrowseNumber);
                int counts = _commentContext.Comment.Where(c => c.ArticleId == article.Id).Count();
                keyValuePairs.Add("commentCounts", counts);
                allArticlesMessage.Add(keyValuePairs);
            }
            return allArticlesMessage;
        }

        public void AddReply(Comment comment)
        {
            string commentName = _userContext.User.Where(u => u.ID == comment.UserId).FirstOrDefault()?.Name;
            Reply reply = new Reply();
            reply.CommentName = commentName;
            reply.Content = comment.Content;
            reply.CreateTime = DateTime.Now;
            reply.ReplyName = comment.CommentName;
            reply.UserId = comment.UserId;
            reply.CommentId = comment.Id;
            _replyContext.Reply.Add(reply);
            _replyContext.SaveChanges();
        }
    }
}
