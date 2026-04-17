using AutoMapper;
using ContactMangerAPI.Models;
using ContactMangerAPI.Models.DTO;
using ContactMangerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactMangerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactInterface _contactInterface;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactsController> _logger;
        public ContactsController(IContactInterface contactInterface, IMapper mapper, ILogger<ContactsController> logger)
        {
            _contactInterface = contactInterface;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        // GET: api/contacts?filterOn=Address&filterQuery=Florida&sortBy=Name&isAscending=True&pageNumber=1&pageSize=10
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn = null, [FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool? isAscending = true,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var contacts = await _contactInterface.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            var contactsDto = _mapper.Map<List<ContactReadDto>>(contacts);

            // Create an exception
            throw new Exception("This is a new exception.");

            return Ok(contactsDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactInterface.GetByIdAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(_mapper.Map<ContactReadDto>(contact));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactCreateDto contactCreateDto)
        {
            var model = _mapper.Map<Contact>(contactCreateDto);

            model = await _contactInterface.CreateAsync(model);

            var contactDto = _mapper.Map<ContactReadDto>(model);

            return CreatedAtAction(nameof(GetById), new { id = contactDto.Id }, contactDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var contact = await _contactInterface.DeleteAsync(id);

            if (contact == null)
                return NotFound();

            var contactDto = _mapper.Map<ContactReadDto>(contact);

            return Ok(contactDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ContactCreateDto contactCreateDto)
        {
            var model = _mapper.Map<Contact>(contactCreateDto);

            model = await _contactInterface.UpdateAsync(id, model);

            if (model == null)
                return NotFound();

            var contactDto = _mapper.Map<ContactReadDto>(model);

            return Ok(contactDto);
        }
    }
}
