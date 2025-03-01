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

        public void Edit(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void Delete(Task task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateComplete(int taskId)
        {
            // Find the task directly by TaskId
            var task = _context.Tasks.FirstOrDefault(t => t.TaskId == taskId);

            if (task != null)
            {
                task.Completed = true;  // Set Completed to 1
                _context.Tasks.Update(task);  // Update the task in the context
                _context.SaveChanges();  // Save changes to the database
            }
        }
    }
}
