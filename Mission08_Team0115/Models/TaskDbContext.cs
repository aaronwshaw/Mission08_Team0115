using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0115.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) 
        { }
        
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
