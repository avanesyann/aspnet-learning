using System.ComponentModel.DataAnnotations;

namespace ContactMangerAPI.Models.DTO
{
    public class ContactCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [Phone]
        [StringLength(15, MinimumLength = 7)]
        public string Phone { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
    }
}
