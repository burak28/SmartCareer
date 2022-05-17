using Microsoft.EntityFrameworkCore;

namespace SmartCareer.DBContext
{
    public class SmartCareerDBContext : DbContext
    {
        public SmartCareerDBContext(DbContextOptions<SmartCareerDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SmartCareerDB");
        }
    }
}