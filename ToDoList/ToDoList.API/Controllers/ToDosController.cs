using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Model;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ToDosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult >GetAll()
        {
            var domainToDos = await _context.ToDos.ToListAsync();
            var dtoToDos = new List<ToDoReadDto>();

            foreach (var toDo in domainToDos)
            {
                dtoToDos.Add(new ToDoReadDto
                {
                    Id = toDo.Id,
                    Name = toDo.Name,
                    Description = toDo.Description,
                    IsCompleted = toDo.IsCompleted,
                    CreatedAt = toDo.CreatedAt
                });
            }

            return Ok(dtoToDos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domainToDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if (domainToDo == null)
                return NotFound();

            var dtoToDo = new ToDoReadDto
            {
                Id = domainToDo.Id,
                Name = domainToDo.Name,
                Description = domainToDo.Description,
                IsCompleted = domainToDo.IsCompleted,
                CreatedAt = domainToDo.CreatedAt
            };

            return Ok(dtoToDo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoCreateDto toDoCreateDto)
        {
            var domainToDo = new ToDo
            {
                Name = toDoCreateDto.Name,
                Description = toDoCreateDto.Description,
                IsCompleted = toDoCreateDto.IsCompleted,
            };

            _context.ToDos.Add(domainToDo);
            await _context.SaveChangesAsync();

            var readToDo = new ToDoReadDto
            {
                Id = domainToDo.Id,
                Name = domainToDo.Name,
                Description = domainToDo.Description,
                IsCompleted = domainToDo.IsCompleted,
                CreatedAt = domainToDo.CreatedAt,
            };

            return CreatedAtAction(nameof(GetById), new { id = readToDo.Id }, readToDo);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ToDoCreateDto createToDo)
        {
            var domainToDo = await _context.ToDos.FirstOrDefaultAsync(x => x.Id == id);

            if (domainToDo == null)
                return NotFound();

            domainToDo.Name = createToDo.Name;
            domainToDo.Description = createToDo.Description;
            domainToDo.IsCompleted = createToDo.IsCompleted;

            await _context.SaveChangesAsync();

            var readToDo = new ToDoReadDto
            {
                Id = domainToDo.Id,
                Name = domainToDo.Name,
                Description = domainToDo.Description,
                IsCompleted = domainToDo.IsCompleted,
                CreatedAt = domainToDo.CreatedAt,
            };

            return Ok(readToDo);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainToDo = await _context.ToDos.FindAsync(id);

            if (domainToDo == null)
                return NotFound();

            _context.Remove(domainToDo);
            await _context.SaveChangesAsync();

            var readToDo = new ToDoReadDto
            {
                Id = domainToDo.Id,
                Name = domainToDo.Name,
                Description = domainToDo.Description,
                IsCompleted = domainToDo.IsCompleted,
                CreatedAt = domainToDo.CreatedAt,
            };

            return Ok(readToDo);
        }
    }
}
