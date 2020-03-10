using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.inputs
{
    /// <summary>
    /// 添加评论回复的实体类
    /// </summary>
    public class AddCommentReply
    {
        public int RepliedId { get; set; }
        public int ArticleId { get; set; }
        public string CommentName { get; set; }
        public string MailBox { get; set; }
        public string Content { get; set; }
        public bool FirstComment { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
