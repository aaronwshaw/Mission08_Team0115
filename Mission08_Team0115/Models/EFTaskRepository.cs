
namespace Mission08_Team0115.Models
{
    public class EFTaskRepository : ITaskRepository
    {
        private TaskDbContext _dbContext;
        public EFTaskRepository(TaskDbContext temp) 
        { 
            _dbContext = temp;
        }

        public List<Task> Tasks => _dbContext.Tasks.ToList() ;

        public List<Category> Categories => _dbContext.Categories.ToList();

        public void AddTask(Task task)
        {
            _dbContext.Tasks.Add(task) ;
            _dbContext.SaveChanges();
        }
    }
}
