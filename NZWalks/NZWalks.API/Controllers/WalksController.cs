using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _repository;
        private readonly IMapper _mapper;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _repository = walkRepository;
        }

        // CREATE Walk
        // POST: /api/Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkCreateDto walkCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to Domain Model
            var domainModel = _mapper.Map<Walk>(walkCreateDto);

            await _repository.CreateAsync(domainModel);

            // Map Domain model to DTO
            return Ok(_mapper.Map<WalkReadDto>(domainModel));
        }

        // GET Walks
        // GET: /api/Walks?filterOn=Name&filterQuery=Track
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walksDomain = await _repository.GetAllAsync(filterOn, filterQuery);

            // Map Domain Model to Dto
            return Ok(_mapper.Map <List<WalkReadDto>>(walksDomain));
        }

        // GET Walk by Id
        // GET: /api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _repository.GetByIdAsync(id);

            if (walkDomain == null)
                return NotFound();

            return Ok(_mapper.Map<WalkReadDto>(walkDomain));
        }

        // Update Walk by Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WalkCreateDto walkCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var walkDomain = _mapper.Map<Walk>(walkCreateDto);

            walkDomain = await _repository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
                return NotFound();

            return Ok(_mapper.Map<WalkReadDto>(walkDomain));
        }

        // Delete Walk by Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await _repository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkReadDto>(walkDomain));
        }
    }
}
