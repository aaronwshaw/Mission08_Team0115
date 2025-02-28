using System.Collections.Generic;
using System.Linq;

namespace Mission08_Team0115.Models
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        IQueryable<Category> Categories { get; }

        public void AddTask(Task task);
        public void UpdateTask(Task task);
        public void DeleteTask(Task task);
        public void Save();
    }
}
