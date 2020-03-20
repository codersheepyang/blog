using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Article
{
    //Article表的实例类
    [Table("Article")]
    public class Article
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ArticleName { get; set; }
        public string Content { get; set; }
        public string InUser { get; set; } 
        public DateTime InDate { get; set; }   
        public int BrowseNumber { get; set; }
        public int ClassificationId { get; set; }
        public bool Status { get; set; }
        public int TagId { get; set; }
        public Comment.Comment Comment { get; set; }
       
    }
}
