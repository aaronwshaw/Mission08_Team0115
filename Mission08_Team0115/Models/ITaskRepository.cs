using System.Collections.Generic;
using System.Linq;

namespace Mission08_Team0115.Models
{
    public interface ITaskRepository
    {
        IQueryable<Task> Tasks { get; }
        IQueryable<Category> Categories { get; }

        void AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(Task task);
        void Save();
    }
}
