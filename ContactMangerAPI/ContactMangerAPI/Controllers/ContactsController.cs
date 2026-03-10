using ContactMangerAPI.Models;
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
            return Ok(await _contactInterface.GetAllAsync());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contact = await _contactInterface.GetByIdAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            var model = await _contactInterface.CreateAsync(contact);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var contact = await _contactInterface.DeleteAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }
    }
}
