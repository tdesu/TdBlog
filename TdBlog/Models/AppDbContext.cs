using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace TdBlog.Models
{
    public class AppDbContext : DbContext
    {
        public const string ConnectionStringName = "TdBlogConnectionString";

        public DbSet<Post> Posts { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}