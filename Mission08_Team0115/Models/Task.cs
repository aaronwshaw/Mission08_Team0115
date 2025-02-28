using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team0115.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string? DueDate { get; set; } //nullable as not said to be required
        [Required]
        public int Quadrant { get; set; }
        public bool? Completed { get; set; } //nullable as not said to be required

        [ForeignKey(name:"CategoryId")]
        public int? CategoryId  { get; set; }
        public Category? Category { get; set; } //FK stuff
    }
}
