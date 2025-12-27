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
        private readonly IRegionRepository _repository;
        private readonly IMapper _mapper;

        public WalksController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // CREATE Walk
        // POST: /api/Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkCreateDto walkCreateDto)
        {
            // Map DTO to Domain Model
            var domainModel = _mapper.Map<Walk>(walkCreateDto);

            return null;
        }
    }
}
