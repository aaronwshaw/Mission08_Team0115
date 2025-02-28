using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mission08_Team0115.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskDbContext _context;

        public EFTaskRepository(TaskDbContext ctx)
        {
            _context = ctx;
        }

        public IQueryable<Task> Tasks => _context.Tasks;
        public IQueryable<Category> Categories => _context.Categories;

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
