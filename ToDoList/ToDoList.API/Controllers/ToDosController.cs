using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Model;
using ToDoList.API.Repositories;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private readonly IToDoRepository _repository;

        public ToDosController(IToDoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var domainToDos = await _repository.GetAllAsync();


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
            var domainToDo = await _repository.GetByIdAsync(id);

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

            domainToDo = await _repository.CreateAsync(domainToDo);

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
            var domainToDo = new ToDo
            {
                Name = createToDo.Name,
                Description = createToDo.Description,
                IsCompleted = createToDo.IsCompleted,
            };

            domainToDo = await _repository.UpdateAsync(id, domainToDo);

            if (domainToDo == null)
                return NotFound();

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
            var domainToDo = await _repository.DeleteAsync(id);

            if (domainToDo == null)
                return NotFound();

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
