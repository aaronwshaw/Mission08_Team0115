using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0115.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) 
        { }
        
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=taskmaster.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasOne(d => d.Category).WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CategoryId);
            });
        }
    }
}
