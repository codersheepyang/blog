using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class ReplyContext:DbContext
    {
        public ReplyContext(DbContextOptions<ReplyContext> options) : base(options) { }

        public DbSet<Reply> Reply { get; set; }  
    }
}
