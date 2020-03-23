using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Comment
{
    [Table("Comment")]
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CommentName { get; set; }
        public string Content { get; set; }
        public bool FirstComment { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
