using System.ComponentModel.DataAnnotations;

namespace Mission08_Team3_7.Models;

public class Task
{
    public class Application
    {
        public int TaskId { get; set; }
        
        [Required]
        public string Task { get; set; }

        public string DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }

        [Required(ErrorMessage = "Please select a major.")]
        public int? CategoryId { get; set; }
        
        public Category? Category { get; set; } // Navigation property
        
        public bool Completed { get; set; }


    }
    
    
}