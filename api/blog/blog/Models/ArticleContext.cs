using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Article
{
    //EF操作类
    public class ArticleContext:DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options)
        {

        }

        public DbSet<Article> Article { get; set; }
    }
}
