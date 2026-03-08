using System.ComponentModel.DataAnnotations;

namespace ContactMangerAPI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
