using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Consumer
{
    
    [Table("Users")]
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SelfIntroduction { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
    }
}
