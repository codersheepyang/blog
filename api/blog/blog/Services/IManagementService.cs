using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Services
{
    public interface IManagementService
    {
        string GetTagByTagId(int tagId);

        List<Dictionary<string, object>> GetAllArticlesMessage(int userId);

        //string articleName, string content, string createUser, int classificationId
        string AddArticle(Article article); 

        //在修改文章时的业务
        string GetArticleById(int articleId); 

        string UpdateArticle(Article article);

        string DeleteArticleById(int articleId);

        List<Dictionary<string, object>> GetAllComments(int userId);

        string DeleteCommentByCommentId(int commentId); 

        string GetAllClassifications(int userId);

        string GetClassificationById(int classificationId);

        string UpdateClassification(Classification classification);

        string DeleteClassificationById(int classificationId); 

        string AddClassification(Classification classification);

        string GetBrowseNumbers(int userId);

        int AddPersonalMessage(User user);

        string GetPersonalMessage(int userId);

        void UpdateCommentStatus(Comment comment);

    }
}
