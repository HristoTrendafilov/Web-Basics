using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpProtocol
{
    public class HttpTestDbContext : DbContext
    {
        public HttpTestDbContext(DbContextOptions options)
            :base(options)
        {
        }
        public HttpTestDbContext()
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=HttpTest;Trusted_Connection=True;");
            }
        }
    }
}
