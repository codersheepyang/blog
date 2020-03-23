using blog.inputs;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace blog.Services
{
    public interface IArticleService
    {
        List<Dictionary<string, object>> GetArticlesByTagId(int tagId);
        string GetAllClassfications();

        string GetAllTags();

        string GetAllArticlesByUpdateTime(int userId);

        string GetAllArticlesByReadCounts(int userId);

        Dictionary<string, Object> GetPersonalData();

        string GetAllClassification(int userId);

        List<Dictionary<string, Object>> GetPigeonhole();

        //void AddPageView(int articleId);

        string GetArticleById(int articleId);

        string AddComment(Comment comment);

        string GetAllCommentsByArticleId(int articleId);
         
        //string AddCommentReply(AddCommentReply addCommentReply);

        string GetLatestArticle();

        AboutMe GetAboutMe();

        void AddReply(Comment comment);




    }
}
