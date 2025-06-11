using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces
{
    public interface IToDoTaskRepository
    {
        Task<ToDoTask?> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime);
        Task CreateTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
        Task DeleteTaskAsync(ToDoTask task);
    }
}