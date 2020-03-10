using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Consumer
{
    //EF操作类
    public class UserContext :DbContext 
    {
        public UserContext (DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }  
    }
}
