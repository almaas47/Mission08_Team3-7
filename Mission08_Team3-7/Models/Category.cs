using System.ComponentModel.DataAnnotations;

namespace Mission08_Team3_7.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }  // Fixed typo here
    }
}