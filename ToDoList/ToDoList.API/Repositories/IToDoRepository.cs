using ToDoList.API.Model;

namespace ToDoList.API.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllAsync(bool? isCompleted, string? sortBy, bool isAscending);
        Task<ToDo?> GetByIdAsync(Guid id);
        Task<ToDo> CreateAsync(ToDo toDo);
         Task<ToDo?> UpdateAsync(Guid id, ToDo toDo);
         Task<ToDo?> DeleteAsync(Guid id);
    }
}
