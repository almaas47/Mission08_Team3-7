using System.ComponentModel.DataAnnotations;

namespace Mission08_Team3_7.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        [Required]
        public string TaskName { get; set; }  // Changed 'Task' to 'TaskName'

        public string DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; } // Navigation property

        public bool Completed { get; set; }
    }
}