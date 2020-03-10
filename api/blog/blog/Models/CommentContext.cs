using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Comment
{

    public class CommentContext:DbContext 
    {
        public CommentContext(DbContextOptions<CommentContext> options):base(options) { }

        public DbSet<Comment> Comment { get; set; }
    }
}
