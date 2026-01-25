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

        public async Task<List<ToDo>> GetAllAsync(bool? isCompleted, string? sortBy, bool isAscending = true)
        {
            var toDosQuery = _context.ToDos.AsQueryable();

            // Filtering
            if (isCompleted.HasValue)
                toDosQuery = toDosQuery.Where(x => x.IsCompleted == isCompleted.Value);

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    toDosQuery = isAscending ? toDosQuery.OrderBy(x => x.Name) : toDosQuery.OrderByDescending(x => x.Name);
                } else if (sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    toDosQuery = isAscending ? toDosQuery.OrderBy(x => x.CreatedAt) : toDosQuery.OrderByDescending(x => x.CreatedAt);
                }
            }

            return await toDosQuery.ToListAsync();
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
