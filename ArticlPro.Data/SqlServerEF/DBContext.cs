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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-GQPMBB1;User Id=sa;Password=123456;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;DataBase=ArticlProDB; Persist Security Info=False");
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<Author> Author { get; set; }

        public DbSet<AuthorPost> AuthorPost { get; set; }
    }
}
