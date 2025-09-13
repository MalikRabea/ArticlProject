using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticlPro.Core;
using Microsoft.EntityFrameworkCore;

namespace ArticlPro.Data.SqlServerEF
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseNpgsql("");
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<AuthorPost> AuthorPost { get; set; }
    }
}
