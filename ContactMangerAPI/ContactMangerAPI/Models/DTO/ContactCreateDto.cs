using System.ComponentModel.DataAnnotations;

namespace ContactMangerAPI.Models.DTO
{
    public class ContactCreateDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum of 2 characters required.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Maximum length should be 15 digits.")]
        public string Phone { get; set; }
        [StringLength(150)]
        public string? Email { get; set; }
        [StringLength(250)]
        public string? Address { get; set; }
    }
}
