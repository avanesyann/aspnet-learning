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
            var regionsDto = new List<RegionReadDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionReadDto()
                {
                    Id = region.Id,
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

            var regionDto = new RegionReadDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ImageUrl = regionDomain.ImageUrl,
            };

            return Ok(regionDto);
        }

        // POST: https://localhost:7192/api/Regions
        [HttpPost]
        public IActionResult Create([FromBody] RegionCreateDto regionCreateDto)
        {
            // Map or Convert DTO to Domain Model
            var domainModel = new Region
            {
                Code = regionCreateDto.Code,
                Name = regionCreateDto.Name,
                ImageUrl = regionCreateDto.ImageUrl,
            };

            // Use Domain Model to create Region
            _context.Regions.Add(domainModel);
            _context.SaveChanges();

            // Map Domain model back to DTO
            var readDto = new RegionReadDto
            {
                Id = domainModel.Id,
                Code = domainModel.Code,
                Name = domainModel.Name,
                ImageUrl = domainModel.ImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RegionUpdateDto regionUpdateDto)
        {
            // Check if region exists
            // On the line below, EF retrieves the entity from the db and starts automatically tracking it, so no need for Update().
            var regionDomainModel = _context.Regions.Find(id);

            if (regionDomainModel == null)
                return NotFound();

            // Map DTO to Domain model
            regionDomainModel.Code = regionUpdateDto.Code;
            regionDomainModel.Name = regionUpdateDto.Name;
            regionDomainModel.ImageUrl = regionUpdateDto.ImageUrl;

            _context.SaveChanges();

            // Convert Domain model to DTO
            var regionDtoModel = new RegionReadDto
            {
                Id = regionDomainModel.Id,
                Code = regionUpdateDto.Code,
                Name = regionUpdateDto.Name,
                ImageUrl = regionUpdateDto.ImageUrl,
            };

            return Ok(regionDtoModel);
        }
    }
}
