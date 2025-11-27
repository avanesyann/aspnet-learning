using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
            var regions = _context.Regions.ToList();

            return Ok(regions);
        }

        // GET: https://localhost:7192/api/Regions/{id}
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // var region = _context.Regions.Find(id);      // Only takes primary key, can't be used with other properties.
            var region = _context.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
                return NotFound();

            return Ok(region);
        }
    }
}
