using ContactMangerAPI.Models;
using ContactMangerAPI.Models.DTO;
using ContactMangerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactMangerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactInterface _contactInterface;
        public ContactsController(IContactInterface contactInterface)
        {
            _contactInterface = contactInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactInterface.GetAllAsync();
            var contactsDto = new List<ContactReadDto>();

            foreach (var contact in contacts)
            {
                contactsDto.Add(new ContactReadDto
                {
                    Id = contact.Id,
                    Name = contact.Name,
                    Address = contact.Address,
                    Email = contact.Email,
                    Phone = contact.Phone,
                });
            }

            return Ok(contactsDto);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactInterface.GetByIdAsync(id);

            if (contact == null)
                return NotFound();

            var contactDto = new ContactReadDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                Email = contact.Email,
                Phone = contact.Phone,
            };

            return Ok(contactDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactCreateDto contactCreateDto)
        {
            var model = new Contact
            {
                Name = contactCreateDto.Name,
                Address = contactCreateDto.Address,
                Email = contactCreateDto.Email,
                Phone = contactCreateDto.Phone,
            };

            model = await _contactInterface.CreateAsync(model);

            var contactDto = new ContactReadDto
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
            };

            return CreatedAtAction(nameof(GetById), new { id = contactDto.Id }, contactDto);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var contact = await _contactInterface.DeleteAsync(id);

            if (contact == null)
                return NotFound();

            var contactDto = new ContactReadDto
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = contact.Address,
                Email = contact.Email,
                Phone = contact.Phone,
            };

            return Ok(contactDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ContactCreateDto contactCreateDto)
        {
            var model = new Contact
            {
                Name = contactCreateDto.Name,
                Address = contactCreateDto.Address,
                Email = contactCreateDto.Email,
                Phone = contactCreateDto.Phone,
            };

            model = await _contactInterface.UpdateAsync(id, model);

            if (model == null)
                return NotFound();

            var contactDto = new ContactReadDto
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address,
                Email = model.Email,
                Phone = model.Phone,
            };

            return Ok(contactDto);
        }
    }
}
