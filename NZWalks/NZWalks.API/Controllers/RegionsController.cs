using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:7192/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: https://localhost:7192/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get data from database (domain models)
            var regionsDomain = _context.Regions.ToList();

            // Map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Code = region.Code,
                    Name = region.Name,
                    ImageUrl = region.ImageUrl,
                });
            }

            // Return DTOs
            return Ok(regionsDto);
        }
        
        // GET: https://localhost:7192/api/Regions/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // var region = _context.Regions.Find(id);      // Only takes primary key, can't be used with other properties.
            var regionDomain = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain == null)
                return NotFound();

            var regionDto = new RegionDto()
            {
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ImageUrl = regionDomain.ImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
