using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            // Map DTO to Domain Model
            var domainModel = _mapper.Map<Walk>(walkCreateDto);

            await _repository.CreateAsync(domainModel);

            // Map Domain model to DTO
            return Ok(_mapper.Map<WalkReadDto>(domainModel));
        }

        // GET Walks
        // GET: /api/Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await _repository.GetAllAsync();

            // Map Domain Model to Dto
            return Ok(_mapper.Map <List<WalkReadDto>>(walksDomain));
        }
    }
}
