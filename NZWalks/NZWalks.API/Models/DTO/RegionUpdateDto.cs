using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class RegionUpdateDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum of 3 characters required."), MaxLength(5, ErrorMessage = "Maximum of 5 characters required.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum of 100 characters required.")]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
    }
}
