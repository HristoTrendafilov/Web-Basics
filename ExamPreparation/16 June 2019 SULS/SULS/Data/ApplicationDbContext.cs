using Microsoft.EntityFrameworkCore;
using SULS.Data.Models;

namespace SULS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Submission> Submissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Problem> Problems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SULS;Integrated Security=true;");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
