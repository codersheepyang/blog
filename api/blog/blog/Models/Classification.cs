using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Classification
{
    [Table("Classification")]
    public class Classification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ClassificationName { get; set; }
        public Article.Article Article { get; set; }
    }
}
