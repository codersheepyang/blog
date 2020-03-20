using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Tag
{
    [Table("Tag")]
    public class Tag
    {
        public int ID { get; set; }
        public string TagName { get; set; }
    }
}
