using ToDoList.API.Model;

namespace ToDoList.API.Repositories
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAllAsync();
        Task<ToDo?> GetByIdAsync(Guid id);
        Task<ToDo> CreateAsync(ToDo toDo);
        // Task<ToDo?> UpdateAsync(Guid id, ToDoCreateDto updateToDo);
        // Task<ToDo?> DeleteAsync(Guid id);
    }
}
