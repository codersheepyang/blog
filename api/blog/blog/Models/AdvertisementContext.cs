using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models.Advertisement
{
    //EF操作类
    public class AdvertisementContext:DbContext 
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options) : base(options)
        {

        }

        public DbSet<Advertisement> Advertisement { get; set; } 
    }
}
