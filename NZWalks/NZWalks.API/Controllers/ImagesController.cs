using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Use repository to upload image
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported extension");
            }

            if (request.File.Length > 1048576)
            {
                ModelState.AddModelError("file", "File size exceeds 10MB");
            }
        }
    }
}
