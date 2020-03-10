using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Classification
{
    public class ClassificationContext:DbContext
    {
        public ClassificationContext(DbContextOptions<ClassificationContext> options) : base(options)
        { }

        public DbSet<Classification> Classification { get; set; }
    }
}
