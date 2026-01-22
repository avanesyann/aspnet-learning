using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Data;
using ToDoList.API.Model;

namespace ToDoList.API.Repositories
{
    public class SQLToDoRepository : IToDoRepository
    {
        private readonly ApplicationDbContext _context;
        public SQLToDoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDo>> GetAllAsync()
        {
            return await _context.ToDos.ToListAsync();
        }
        public async Task<ToDo?> GetByIdAsync(Guid id)
        {
            return await _context.ToDos.FindAsync(id);
        }
        public async Task<ToDo> CreateAsync(ToDo toDo)
        {
            await _context.ToDos.AddAsync(toDo);
            await _context.SaveChangesAsync();

            return toDo;
        }
        public async Task<ToDo?> UpdateAsync(Guid id, ToDo toDo)
        {
            var domainToDo = await _context.ToDos.FindAsync(id);

            if (domainToDo == null)
            {
                return null;
            }

            domainToDo.Name = toDo.Name;
            domainToDo.Description = toDo.Description;
            domainToDo.IsCompleted = toDo.IsCompleted;

            await _context.SaveChangesAsync();

            return toDo;
        }
        public async Task<ToDo?> DeleteAsync(Guid id)
        {
            var domainToDo = await _context.ToDos.FindAsync(id);

            if (domainToDo == null)
                return null;

            _context.ToDos.Remove(domainToDo);
            await _context.SaveChangesAsync();

            return domainToDo;
        }
    }
}
