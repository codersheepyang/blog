using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    [Table("Reply")]
    public class Reply
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string CommentName { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string ReplyName { get; set; }
    }
}
