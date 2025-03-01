using System.Collections.Generic;
using System.Linq;

namespace Mission08_Team0115.Models
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        IQueryable<Category> Categories { get; }

        public void AddTask(Task task);
        public void Edit(Task task);
        public void Delete(Task task);
        public void Save();

        void UpdateComplete(int taskId);  // Update only the completion status
    }
}
