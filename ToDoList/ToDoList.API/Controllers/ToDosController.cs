using AutoMapper;
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
        private readonly IMapper _mapper;

        public ToDosController(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: /api/ToDos?filterOn=isCompleted&filterQuery=True
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] bool? isCompleted)
        {
            var domainToDos = await _repository.GetAllAsync(isCompleted);

            var dtoToDos = _mapper.Map<List<ToDoReadDto>>(domainToDos);

            return Ok(dtoToDos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var domainToDo = await _repository.GetByIdAsync(id);

            if (domainToDo == null)
                return NotFound();

            var dtoToDo = _mapper.Map<ToDoReadDto>(domainToDo);

            return Ok(dtoToDo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoCreateDto toDoCreateDto)
        {
            var domainToDo = _mapper.Map<ToDo>(toDoCreateDto);

            domainToDo = await _repository.CreateAsync(domainToDo);

            var readToDo = _mapper.Map<ToDoReadDto>(domainToDo);

            return CreatedAtAction(nameof(GetById), new { id = readToDo.Id }, readToDo);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ToDoCreateDto createToDo)
        {
            var domainToDo = _mapper.Map<ToDo>(createToDo);

            domainToDo = await _repository.UpdateAsync(id, domainToDo);

            if (domainToDo == null)
                return NotFound();

            var readToDo = _mapper.Map<ToDoReadDto>(domainToDo);

            return Ok(readToDo);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainToDo = await _repository.DeleteAsync(id);

            if (domainToDo == null)
                return NotFound();

            var readToDo = _mapper.Map<ToDoReadDto>(domainToDo);

            return Ok(readToDo);
        }
    }
}
