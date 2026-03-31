using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    // https://localhost:7192/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(ApplicationDbContext context, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _context = context;
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: https://localhost:7192/api/Regions
        [HttpGet]
        // [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception.");

                // Get data from database (domain models)
                var regionsDomain = await _regionRepository.GetAllAsync();

                _logger.LogInformation("GetAllRegions Action Method was invoked");

                // Map domain models to DTOs
                //var regionsDto = new List<RegionReadDto>();
                //foreach (var region in regionsDomain)
                //{
                //    regionsDto.Add(new RegionReadDto()
                //    {
                //        Id = region.Id,
                //        Code = region.Code,
                //        Name = region.Name,
                //        ImageUrl = region.ImageUrl,
                //    });
                //}

                // Map domain models to DTOs
                var regionsDto = _mapper.Map<List<RegionReadDto>>(regionsDomain);

                _logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDto)}");

                // Return DTOs
                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        
        // GET: https://localhost:7192/api/Regions/{id}
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
                return NotFound();

            return Ok(_mapper.Map<RegionReadDto>(regionDomain));
        }

        // POST: https://localhost:7192/api/Regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] RegionCreateDto regionCreateDto)
        {
            // Map or Convert DTO to Domain Model
            // _mapper.Map<[Destination]>([Source]);
            var domainModel = _mapper.Map<Region>(regionCreateDto);

            // Use Domain Model to create Region
            domainModel = await _regionRepository.CreateAsync(domainModel);

            // Map Domain model back to DTO
            var readDto = _mapper.Map<RegionReadDto>(domainModel);

            return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, readDto);
        }

        // UPDATE: https://localhost:7192/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] RegionUpdateDto regionUpdateDto)
        {
            var regionDomainModel = _mapper.Map<Region>(regionUpdateDto);

            regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
                return NotFound();

            // Convert Domain model to DTO
            var regionDtoModel = _mapper.Map<RegionReadDto>(regionDomainModel);

            return Ok(regionDtoModel);
        }

        // DELETE: https://localhost:7192/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteAsync(id);

            if (region == null)
                return NotFound();

            var regionDto = _mapper.Map<RegionReadDto>(region);

            return Ok(regionDto);
        }
    }
}
