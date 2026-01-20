using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.Model
{
    public class ToDoCreateDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
