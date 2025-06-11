namespace ToDoList.Application.Interfaces
{
    public interface IToDoTaskRepository : IGenericRepository<ToDoTask>
    {
        Task<ToDoTask?> GetTaskByIdAsync(Guid id);
        Task<IEnumerable<ToDoTask>> GetTasksByDueDateTimeAsync(DateTime dueDateTime);
        Task CreateTaskAsync(ToDoTask task);
        Task UpdateTaskAsync(ToDoTask task);
        Task DeleteTaskByIdAsync(Guid id);
    }
}