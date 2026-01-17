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
    }
}
