using System.ComponentModel.DataAnnotations;

namespace ContactMangerAPI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required]          // must exist in DB
        [MaxLength(100)]    // DB constraint
        public string Name { get; set; }
        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
