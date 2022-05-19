using Microsoft.EntityFrameworkCore;

namespace SmartCareer.DBContext
{
    public class SmartCareerDBContext : DbContext
    {
        public SmartCareerDBContext(DbContextOptions<SmartCareerDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                "Server=localhost;Port=49153;Database=SmartCareer;User Id=postgres;Password=postgrespw");
            }
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }
    }
}