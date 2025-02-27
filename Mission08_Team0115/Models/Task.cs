using System.ComponentModel.DataAnnotations;

namespace Mission08_Team0115.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string DueDate { get; set; }
        [Required]
        public int Quadrant { get; set; }
        public int CategoryId  { get; set; }
        public bool Completed { get; set; }
    }
}
