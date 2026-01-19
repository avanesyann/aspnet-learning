using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            var domainToDos = _context.ToDos.ToList();
            var dtoToDos = new List<ToDoReadDto>();

            if (domainToDos == null)
            {
                return NotFound();
            }

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            var domainToDo = _context.ToDos.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] ToDoCreateDto toDoCreateDto)
        {
            var domainToDo = new ToDo
            {
                Name = toDoCreateDto.Name,
                Description = toDoCreateDto.Description,
                IsCompleted = toDoCreateDto.IsCompleted,
                CreatedAt = toDoCreateDto.CreatedAt
            };

            if (domainToDo == null)
                return NotFound();

            _context.ToDos.Add(domainToDo);
            _context.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] ToDoCreateDto createToDo)
        {
            var domainToDo = _context.ToDos.FirstOrDefault(x => x.Id == id);

            if (domainToDo == null)
                return NotFound();

            domainToDo.Name = createToDo.Name;
            domainToDo.Description = createToDo.Description;
            domainToDo.IsCompleted = createToDo.IsCompleted;
            domainToDo.CreatedAt = createToDo.CreatedAt;

            _context.SaveChanges();

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var domainToDo = _context.ToDos.Find(id);

            if (domainToDo == null)
                return NotFound();

            _context.Remove(domainToDo);
            _context.SaveChanges();

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
