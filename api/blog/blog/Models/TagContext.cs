using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Tag 
{
    public class TagContext:DbContext
    {
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        { }

        public DbSet<Tag> Tag { get; set; }
    }
}
