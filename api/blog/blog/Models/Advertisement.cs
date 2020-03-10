using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Advertisement 
{
    
    [Table("Advertisement")]
    public class Advertisement 
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Advertiser { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
