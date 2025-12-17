using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:7192/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRegionRepository _regionRepository;

        public RegionsController(ApplicationDbContext context, IRegionRepository regionRepository)
        {
            _context = context;
            _regionRepository = regionRepository;
        }

        // GET: https://localhost:7192/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get data from database (domain models)
            var regionsDomain = await _regionRepository.GetAllAsync();

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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([FromBody] RegionCreateDto regionCreateDto)
        {
            // Map or Convert DTO to Domain Model
            var domainModel = new Region
            {
                Code = regionCreateDto.Code,
                Name = regionCreateDto.Name,
                ImageUrl = regionCreateDto.ImageUrl,
            };

            // Use Domain Model to create Region
            domainModel = await _regionRepository.CreateAsync(domainModel);

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

        // UPDATE: https://localhost:7192/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionUpdateDto regionUpdateDto)
        {
            var regionDomainModel = new Region
            {
                Code = regionUpdateDto.Code,
                Name = regionUpdateDto.Name,
                ImageUrl = regionUpdateDto.ImageUrl,
            };

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
                return NotFound();

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

        // DELETE: https://localhost:7192/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);

            if (region == null)
                return NotFound();

            var regionDto = new RegionReadDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                ImageUrl = region.ImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
